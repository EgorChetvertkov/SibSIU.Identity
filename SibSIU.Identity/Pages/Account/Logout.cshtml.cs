using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Server.AspNetCore;

namespace SibSIU.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        [BindProperty]
        public string? ReturnURL { get; set; }
        [BindProperty]
        public bool LogoutFromAll { get; set; }

        public IActionResult OnGet(string? post_logout_redirect_uri)
        {
            ReturnURL = post_logout_redirect_uri ?? "/";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (LogoutFromAll)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return SignOut(new AuthenticationProperties { RedirectUri = ReturnURL }, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
