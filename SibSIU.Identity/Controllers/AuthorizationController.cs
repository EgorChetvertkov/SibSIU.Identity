﻿using Microsoft.AspNetCore;
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
                return await Authenticate(user.Data, request, cancellationToken);

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
        if (result is null)
        {
            return GetForbid(Errors.AccessDenied, "Вы не прошли аутентификацию или просрочили действие аутентификации");
        }

        var userName = result.Principal?.GetClaim(ClaimNames.Subject);
        var user = await userInfo.Handle(new(userName ?? string.Empty), cancellationToken);
        if (user.IsFailure)
        {
            return GetForbid(Errors.ServerError, "Не удалось получить информацию о пользователе");
        }

        var scopes = result.Principal?.GetScopes() ?? [];

        var identity = OpenIdDictHandlers.GetIdentity(user.Data);
        identity.SetScopes(scopes);
        identity.SetResources(await scopeManager.ListResourcesAsync(scopes, cancellationToken).ToListAsync(cancellationToken));
        identity.SetDestinations(static c => OpenIdDictHandlers.GetDestinations(c));

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("~/connect/userinfo"), HttpPost("~/connect/userinfo")]
    public async Task<IActionResult> UserInfo(CancellationToken cancellationToken)
    {
        // TODO : Create handler for get claim list by userName
        var userName = User.GetClaim(ClaimNames.Subject);
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
            [ClaimNames.Subject] = user.Data.UserName,
            [ClaimNames.EmailAddress] = user.Data.Email,
            [ClaimNames.EmailVerified] = user.Data.EmailConfirmed,
            [ClaimNames.FirstName] = user.Data.FirstName,
            [ClaimNames.FamilyName] = user.Data.LastName,
            [ClaimNames.FamilyName] = user.Data.Patronymic,
            [ClaimNames.BirthDate] = user.Data.BirthOfDate,
            [ClaimNames.Gender] = user.Data.Gender.Name,
            [ClaimNames.PhoneNumber] = user.Data.PhoneNumber
        };

        foreach (var work in user.Data.Works)
        {
            claims.Add(ClaimNames.Worker, work.WorkPlaceId);
        }

        foreach (var work in user.Data.Pupils)
        {
            claims.Add(ClaimNames.Pupil, work.PupilId);
        }

        foreach (var work in user.Data.Partners)
        {
            claims.Add(ClaimNames.Partner, work.PartnerId);
        }

        foreach (var work in user.Data.Students)
        {
            claims.Add(ClaimNames.Student, work.StudentId);
        }

        foreach (var item in user.Data.Claims)
        {
            claims.Add(item.ClaimType.Name, item.Value);
        }

        foreach (var role in user.Data.Roles)
        {
            claims.Add(ClaimNames.Role, role.Name);
        }

        return Ok(claims);
    }

    private async Task<SignInResult> Authenticate(UserDetails user, OpenIddictRequest request, CancellationToken cancellationToken)
    {
        var scopes = request.GetScopes();

        var identity = OpenIdDictHandlers.GetAuthCodeIdentity(user);
        identity.SetScopes(scopes);
        identity.SetResources(await scopeManager.ListResourcesAsync(scopes, cancellationToken).ToListAsync(cancellationToken));
        identity.SetDestinations(static c => OpenIdDictHandlers.GetDestinations(c));

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
