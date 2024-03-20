using SibSIU.Identity.Models.Scopes;

using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.ClaimTypes;
public sealed class ClaimTypeDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public bool IncludeInAccessToken { get; set; }
    [Required]
    public bool IncludeInIdentityToken { get; set; }
    [Required]
    public List<ScopeItem> Scopes { get; set; } = [];
}
