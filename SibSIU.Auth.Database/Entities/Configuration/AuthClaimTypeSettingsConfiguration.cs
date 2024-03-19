using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class AuthClaimTypeSettingsConfiguration : EntityWithUlidIdConfiguration<AuthClaimTypeScopes>
{
    public override void Configure(EntityTypeBuilder<AuthClaimTypeScopes> builder)
    {
        base.Configure(builder);
    }
}
