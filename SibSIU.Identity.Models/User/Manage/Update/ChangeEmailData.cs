using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage.Update;
public class ChangeEmailData
{
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}
