using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage;
public sealed class ForgotPasswordData
{
    [Required, EmailAddress]
    public required string Email { get; set; }
}
