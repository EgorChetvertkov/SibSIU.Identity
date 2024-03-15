using SibSIU.Identity.Models.Genders;

using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage;
public sealed class CreateUserData
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null;
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; } = null;
    [Required]
    public DateTimeOffset BirthOfDate { get; set; } = default!;
    [Required]
    public GenderItem Gender { get; set; } = null!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
