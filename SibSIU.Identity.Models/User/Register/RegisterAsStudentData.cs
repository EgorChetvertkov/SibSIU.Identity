using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Register;
public sealed class RegisterAsStudentData
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null;
    [Required]
    public int DeanCode { get; set; }
    [Required]
    public DateTime BirthOfDate { get; set; } = default!;
    [Required]
    public string GenderId { get; set; } = null!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
