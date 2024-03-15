using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

using SibSIU.Core.Names;
using SibSIU.Domain.UserManager.Users.Queries.GetUserInfoByUserName;
using SibSIU.Identity.Infrastructure;
using SibSIU.Identity.Infrastructure.Models;

using System.Security.Claims;

using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SibSIU.Identity.Pages.Account
{
    [Authorize]
    public class VerifyModel(
        IGetUserInfoByUserNameHandler userInfo,
        IOpenIddictApplicationManager applicationManager,
        IOpenIddictScopeManager scopeManager) : PageModel
    {
        [BindProperty]
        public VerifyViewModel Model { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            if (request is null)
            {
                Model = new()
                {
                    Error = "Запрос не содержит нужных параметров"
                };
                return Page();
            }

            if (string.IsNullOrEmpty(request.UserCode))
            {
                Model = new()
                {
                    Error = "Укажите код пользователя"
                };
                return Page();
            }

            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var clientId = result.Principal.GetClaim(Claims.ClientId);
                if (clientId is null)
                {
                    Model = new()
                    {
                        Error = "Не удалось обнаружить клиента"
                    };
                    return Page();
                }

                var application = await applicationManager.FindByClientIdAsync(clientId, cancellationToken);
                if (application is null)
                {
                    Model = new()
                    {
                        Error = "Не удалось обнаружить приложение"
                    };
                    return Page();
                }

                var applicationName = await applicationManager.GetLocalizedDisplayNameAsync(application, cancellationToken);
                if (applicationName is null)
                {
                    Model = new()
                    {
                        Error = "Не удалось обнаружить приложение"
                    };
                    return Page();
                }

                string userCode = Model.UserCode;
                Model = new()
                {
                    ApplicationName = applicationName,
                    Scope = string.Join(" ", result.Principal.GetScopes()),
                    UserCode = userCode,
                };
                return Page();
            }

            Model.Error = "Код пользователя указан не верно";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Form["submit.Accept"]) &&
                string.IsNullOrEmpty(HttpContext.Request.Form["submit.Deny"]))
            {
                var user = await userInfo.Handle(new(User.GetStringUserId(ClaimNames.Id)), cancellationToken);
                if (user.IsFailure)
                {
                    Model = new()
                    {
                        Error = user.Error.Message
                    };
                    return Page();
                }

                var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                if (result.Succeeded)
                {
                    var scopes = result.Principal.GetScopes();

                    var identity = OpenIdDictHandlers.GetIdentity(user.Data);
                    identity.SetScopes(scopes);
                    identity.SetResources(await scopeManager.ListResourcesAsync(scopes, cancellationToken).ToListAsync(cancellationToken));
                    identity.SetDestinations(static c => OpenIdDictHandlers.GetDestinations(c));

                    return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }


                Model = new()
                {
                    Error = "Указанный код пользователя недействителен. Пожалуйста, убедитесь, что вы ввели его правильно."
                };
                return Page();
            }
            else
            {
                Model = new()
                {
                    Error = "Необходимо дать согласие для продолжение потока авторизации"
                };
                return Page();
            }
        }
    }
}
