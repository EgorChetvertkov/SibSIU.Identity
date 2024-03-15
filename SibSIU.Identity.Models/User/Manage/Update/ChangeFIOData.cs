using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage.Update;
public sealed class ChangeFIOData
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; }
}
