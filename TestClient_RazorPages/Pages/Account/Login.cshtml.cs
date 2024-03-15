using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestClient_RazorPages.Pages.Account
{
    public class LoginModel : PageModel
    {
        public ChallengeResult OnGet(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
            return Challenge(authenticationProperties, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
