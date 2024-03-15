using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Register;
public sealed class RegisterAsPupilData
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
    public DateTime BirthOfDate { get; set; } = default!;
    [Required]
    public string GenderId { get; set; } = null!;
    [Required]
    public string SchoolId { get; set; } = null!;
    [Required]
    public char ClassLitter { get; set; }
    [Required]
    [Range(1, 11)]
    public int ClassNumber { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
