using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.ClaimTypes;
public sealed class ClaimTypeDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
