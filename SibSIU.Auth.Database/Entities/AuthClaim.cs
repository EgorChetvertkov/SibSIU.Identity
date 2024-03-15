using SibSIU.Core.Database.EF.Entities;
using SibSIU.UserData.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.Auth.Database.Entities;

public sealed class AuthClaim : EntityWithUlidId
{
    public required string Value { get; set; }
    public Ulid UserId { get; set; }
    public Ulid ScopeId { get; set; }
    public Ulid ClaimTypeId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(ScopeId))]
    public Scope Scope { get; set; } = null!;
    [ForeignKey(nameof(ClaimTypeId))]
    public AuthClaimType ClaimType { get; set; } = null!;
}