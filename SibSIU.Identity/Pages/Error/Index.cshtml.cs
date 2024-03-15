using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SibSIU.Identity.Pages.Error
{
    public class _404Model : PageModel
    {
        [BindProperty]
        public HttpErrorPage Model { get; set; }

        public void OnGet(int statusCode)
        {
            Model = HttpErrorPage.GetError(statusCode);
        }
    }
}
