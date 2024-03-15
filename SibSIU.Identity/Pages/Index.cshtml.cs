using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SibSIU.Identity.Pages;
public class IndexModel : PageModel
{
    [BindProperty]
    public string[] UserName { get; set; } = [];

    public IActionResult OnPost()
    {
        return UserName is null ? Page() : Page();
    }
}
