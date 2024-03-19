using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.Auth.Database.Entities;

public sealed class AuthClaimType : EntityWithUlidId
{
    public required string Name { get; set; }
    public required bool IncludeInAccessToken { get; set; }
    public required bool IncludeInIdentityToken { get; set; }

    public ICollection<AuthClaim> Claims { get; set; } = [];
    public ICollection<AuthClaimTypeScopes> Settings { get; set; } = [];
}