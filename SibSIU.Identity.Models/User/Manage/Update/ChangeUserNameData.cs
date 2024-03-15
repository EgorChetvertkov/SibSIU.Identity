using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage.Update;
public sealed class ChangeUserNameData
{
    [Required]
    public string NewUserName { get; set; } = null!;
}
