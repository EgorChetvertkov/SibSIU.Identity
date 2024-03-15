using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class AuthClaimConfiguration : EntityWithUlidIdConfiguration<AuthClaim>
{
    public override void Configure(EntityTypeBuilder<AuthClaim> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Value).IsRequired().HasMaxLength(256);
    }
}
