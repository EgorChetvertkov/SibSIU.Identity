using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Abstractions;
using SibSIU.Identity.Infrastructure;

namespace SibSIU.Identity.Pages.Account
{
    [Authorize]
    public class ConsentModel : PageModel
    {
        [BindProperty]
        public string? ReturnURL { get; set; }

        public IActionResult OnGet(string? returnUrl)
        {
            ReturnURL = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Request.Form["submit.Accept"]) &&
                string.IsNullOrEmpty(HttpContext.Request.Form["submit.Deny"]))
            {
                User.SetClaim(ConstClaimNames.Consent, ConstClaimNames.ConsentYes);
            }
            else
            {
                User.SetClaim(ConstClaimNames.Consent, ConstClaimNames.ConsentNo);
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, User);

            return ReturnURL is null ?
                    RedirectToPage("/Index") :
                    Redirect(ReturnURL);
        }
    }
}
