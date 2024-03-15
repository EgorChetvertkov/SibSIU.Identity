using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class AuthClaimTypeConfiguration : EntityWithUlidIdConfiguration<AuthClaimType>
{
    public override void Configure(EntityTypeBuilder<AuthClaimType> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
    }
}
