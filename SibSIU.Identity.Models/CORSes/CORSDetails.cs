using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.CORSes;
public sealed class CORSDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string Origin { get; set; } = null!;
}
