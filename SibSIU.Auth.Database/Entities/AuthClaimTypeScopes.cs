using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.Auth.Database.Entities;
public sealed class AuthClaimTypeScopes : EntityWithUlidId
{
    public Ulid ClaimTypeId { get; set; }
    public Ulid ScopeId { get; set; }

    [ForeignKey(nameof(ClaimTypeId))]
    public AuthClaimType ClaimType { get; set; } = null!;
    [ForeignKey(nameof(ScopeId))]
    public Scope Scope { get; set; } = null!;
}
