using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestClient_RazorPages.Pages.Account
{
    public class LogoutModel : PageModel
    {
        [BindProperty]
        public string? ReturnURL { get; set; }
        [BindProperty]
        public bool LogoutFromAll { get; set; }

        public PageResult OnGet()
        {
            ReturnURL = Url.Page("/Index", pageHandler: null, values: null, protocol: Request.Scheme);
            return Page();
        }

        public SignOutResult OnPostAsync()
        {
            string[] signOutsSchemes = new string[2];
            signOutsSchemes[0] = CookieAuthenticationDefaults.AuthenticationScheme;

            if (LogoutFromAll)
            {
                signOutsSchemes[1] = OpenIdConnectDefaults.AuthenticationScheme;
            }

            return SignOut(new AuthenticationProperties { RedirectUri = ReturnURL }, signOutsSchemes);
        }
    }
}
