using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Claims;
public sealed class AddClaimData
{
    [Required]
    public Ulid ClaimTypeId { get; set; }
    [Required]
    public Ulid ScopeId { get; set; }
    [Required]
    public string Value { get; set; } = null!;
}
