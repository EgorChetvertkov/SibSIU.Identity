using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SibSIU.Domain.UserManager.Users.Commands.Login;
using SibSIU.Identity.Infrastructure;
using SibSIU.Identity.Models.User;
using SibSIU.Identity.TempDataModel;

namespace SibSIU.Identity.Pages.Account
{
    public class LoginModel(ILoginHandler loginHandler) : PageModel
    {
        [BindProperty]
        public string? ReturnURL { get; set; }
        [BindProperty]
        public LoginData Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string? returnUrl)
        {
            ReturnURL = returnUrl;

            if (User.Identity?.IsAuthenticated is true)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await loginHandler.Handle(new(Input.UserName, Input.Password), cancellationToken);

            if (result.IsSuccess)
            {
                ToastMessage toast = new()
                {
                    Class = result.Data.IsWarning ? "text-bg-warning" : "text-bg-primary",
                    Message = result.Data.Message
                };

                TempData.Set<ToastMessage>(TempDataExtensions.BaseKey, toast);

                DateTime now = DateTime.UtcNow;
                AuthenticationProperties properties = new()
                {
                    IsPersistent = Input.RememberMe,
                    AllowRefresh = true,
                    IssuedUtc = now,
                    ExpiresUtc = now.AddDays(30),
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Data.Principal, properties);

                return ReturnURL is null ?
                    RedirectToPage("/Index") :
                    Redirect(ReturnURL);
            }
            else
            {
                return RefreshPage(result.Error.Message);
            }
        }

        private PageResult RefreshPage(string errorMessage)
        {
            ModelState.AddModelError("", errorMessage);
            ToastMessage toast = new()
            {
                Class = "text-bg-danger",
                Message = "Проверьте корректность ввода"
            };

            TempData.Set<ToastMessage>(TempDataExtensions.BaseKey, toast);
            return Page();
        }
    }
}
