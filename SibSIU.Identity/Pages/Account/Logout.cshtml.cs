using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Server.AspNetCore;

namespace SibSIU.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public SignOutResult OnGetAsync()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
