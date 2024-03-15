using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage.Update;
public sealed class ChangePasswordByAdminData
{
    [Required, DataType(DataType.Password), MinLength(8)]
    public string NewPassword { get; set; } = null!;
    [Required, DataType(DataType.Password), MinLength(8), Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; } = null!;
}
