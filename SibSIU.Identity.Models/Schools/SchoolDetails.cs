using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.Schools;
public sealed class SchoolDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    [MaxLength(512)]
    public string FullName { get; set; } = null!;
    [Required]
    [MaxLength(128)]
    public string ShortName { get; set; } = null!;
}
