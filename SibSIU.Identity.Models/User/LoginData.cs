using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User;
public sealed class LoginData
{
    [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "UserNameRequired")]
    [Display(ResourceType = typeof(Resources.Resource), Name = "UserName")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordRequired")]
    [Display(ResourceType = typeof(Resources.Resource), Name = "Password")]
    [MinLength(8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "MinPasswordLength8")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Display(ResourceType = typeof(Resources.Resource), Name = "RememberMe")]
    public bool RememberMe { get; set; }
}
