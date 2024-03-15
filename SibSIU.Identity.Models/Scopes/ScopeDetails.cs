using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.Scopes;
public sealed class ScopeDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
