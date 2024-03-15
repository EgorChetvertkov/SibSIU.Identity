using SibSIU.Identity.Models.Genders;
using SibSIU.Identity.Models.Organizations;
using SibSIU.Identity.Models.Posts;

using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Register;
public sealed class RegisterAsPartnerData
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null;
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; } = null;
    [Required]
    public DateTime? BirthOfDate { get; set; } = default!;
    [Required]
    public string GenderId { get; set; } = null!;
    [Required]
    public string OrganizationId { get; set; } = null!;
    [Required]
    public string PostId { get; set; } = null!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
