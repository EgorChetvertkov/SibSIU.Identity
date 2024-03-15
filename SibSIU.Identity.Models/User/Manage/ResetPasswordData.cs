using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage;
public sealed class ResetPasswordData
{
    [Required]
    public required string UserId { get; set; }
    [Required]
    public required string Code { get; set; }
    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }
    [Required, Compare(nameof(Password)), DataType(DataType.Password)]
    public required string ConfirmedPassword { get; set; }
}
