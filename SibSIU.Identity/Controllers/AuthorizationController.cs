using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

using SibSIU.Core.Names;
using SibSIU.Domain.UserManager.Users.Queries.GetUserInfoByUserName;
using SibSIU.Identity.Infrastructure;
using SibSIU.Identity.Models.User.Manage;

using System.Security.Claims;
using System.Web;

using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorizationController(
    IGetUserInfoByUserNameHandler userInfo,
    IOpenIddictApplicationManager applicationManager,
    IOpenIddictScopeManager scopeManager) : ControllerBase
{
    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    public async Task<IActionResult> Authorize(CancellationToken cancellationToken)
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (request is null)
        {
            return GetForbid(Errors.ServerError, "Запрос не поддерживается");
        }

        if (request.ClientId is null)
        {
            return GetForbid(Errors.InvalidClient, "Не удалось корректно обработать запрос. Запрос не содержит client_id");
        }

        var parameters = OpenIdDictHandlers.ParseOAuthParameters(HttpContext, [Parameters.Prompt]);

        if (request.HasPrompt(Prompts.Login))
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = OpenIdDictHandlers.BuildRedirectUrl(HttpContext.Request, parameters)
            }, [CookieAuthenticationDefaults.AuthenticationScheme]);
        }

        AuthenticateResult authenticationResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!OpenIdDictHandlers.IsAuthenticated(authenticationResult, request))
        {
            return Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = OpenIdDictHandlers.BuildRedirectUrl(HttpContext.Request, parameters)
            }, [CookieAuthenticationDefaults.AuthenticationScheme]);
        }

        var application = await applicationManager.FindByClientIdAsync(request.ClientId, cancellationToken);
        if (application is null)
        {
            return GetForbid(Errors.ServerError, "Не удалось обнаружить информацию о вызывающем клиенте");
        }

        var consentClaim = authenticationResult.Principal?.GetClaim(ConstClaimNames.Consent);
        if (consentClaim != ConstClaimNames.ConsentYes || request.HasPrompt(Prompts.Consent))
        {
            var returnUrl = HttpUtility.UrlEncode(OpenIdDictHandlers.BuildRedirectUrl(HttpContext.Request, parameters));
            var consentRedirectUrl = $"/Account/Consent?returnUrl={returnUrl}";

            return Redirect(consentRedirectUrl);
        }

        var userName = authenticationResult.Principal?.GetClaim(ClaimNames.UserName);
        var user = await userInfo.Handle(new(userName ?? string.Empty), cancellationToken);
        if (user.IsFailure)
        {
            return GetForbid(Errors.ServerError, "Не удалось получить информацию о пользователе");
        }

        var client = await applicationManager.GetIdAsync(application);
        if (client is null)
        {
            return GetForbid(Errors.ServerError, "Не удалось получить информацию о приложении");
        }

        switch (await applicationManager.GetConsentTypeAsync(application, cancellationToken))
        {
            case ConsentTypes.Explicit when request.HasPrompt(Prompts.None):
            case ConsentTypes.Systematic when request.HasPrompt(Prompts.None):
                return GetForbid(Errors.ConsentRequired, "Требуется интерактивное согласие пользователя");

            case ConsentTypes.Implicit:
            case ConsentTypes.External:
            case ConsentTypes.Explicit when !request.HasPrompt(Prompts.Consent):
                return await Authenticate(user.Data, request, client, cancellationToken);

            default:
                return GetForbid(Errors.ConsentRequired, "Неизвестный поток");
        }
    }

    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange(CancellationToken cancellationToken)
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (request is null)
        {
            return GetForbid(Errors.ServerError, "Запрос не поддерживается");
        }

        if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType())
        {
            return GetForbid(Errors.UnsupportedGrantType,
                "Поддерживается только поток авторизации, поток учетных данных клиента и refresh-token");
        }

        var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        var userName = result.Principal?.GetClaim(Claims.Subject);
        var user = await userInfo.Handle(new(userName ?? string.Empty), cancellationToken);
        if (user.IsFailure)
        {
            return GetForbid(Errors.ServerError, "Не удалось получить информацию о пользователе");
        }

        var scopes = request.GetScopes();

        var identity = OpenIdDictHandlers.GetIdentity(user.Data);
        identity.SetScopes(scopes);
        identity.SetResources(await scopeManager.ListResourcesAsync(scopes, cancellationToken).ToListAsync(cancellationToken));
        identity.SetDestinations(c => OpenIdDictHandlers.GetDestinations(c));

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("~/connect/userinfo"), HttpPost("~/connect/userinfo")]
    public async Task<IActionResult> UserInfo(CancellationToken cancellationToken)
    {

        var userName = User.GetClaim(Claims.Subject);
        var user = await userInfo.Handle(new(userName ?? string.Empty), cancellationToken);
        if (user.IsFailure)
        {
            return Challenge(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "Вы не авторизованы. Повторно войдите в систему и повторите попытку"
                }));
        }

        var claims = new Dictionary<string, object>(StringComparer.Ordinal)
        {
            [Claims.Subject] = user.Data.UserName,
            [Claims.Email] = user.Data.Email,
            [Claims.EmailVerified] = user.Data.EmailConfirmed,
            [Claims.Name] = user.Data.FirstName,
            [Claims.MiddleName] = user.Data.LastName,
            [Claims.FamilyName] = user.Data.Patronymic,
            [Claims.Birthdate] = user.Data.BirthOfDate,
            [Claims.Gender] = user.Data.Gender.Name,
            [Claims.PhoneNumber] = user.Data.PhoneNumber
        };

        foreach (var work in user.Data.Works)
        {
            claims.Add("workers", work.WorkPlaceId);
        }

        foreach (var work in user.Data.Pupils)
        {
            claims.Add("pupils", work.PupilId);
        }

        foreach (var work in user.Data.Partners)
        {
            claims.Add("partners", work.PartnerId);
        }

        foreach (var work in user.Data.Students)
        {
            claims.Add("students", work.StudentId);
        }

        foreach (var item in user.Data.Claims)
        {
            claims.Add(item.ClaimType.Name, item.Value);
        }

        return Ok(claims);
    }

    private async Task<SignInResult> Authenticate(UserDetails user, OpenIddictRequest request, string client, CancellationToken cancellationToken)
    {
        var scopes = request.GetScopes();

        var identity = OpenIdDictHandlers.GetAuthCodeIdentity(user);
        identity.SetScopes(scopes);
        identity.SetResources(await scopeManager.ListResourcesAsync(scopes, cancellationToken).ToListAsync(cancellationToken));

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private ForbidResult GetForbid(string error, string description) => Forbid(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = error,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = description
                }));
}
