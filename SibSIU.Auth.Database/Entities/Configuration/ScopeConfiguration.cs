using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class ScopeConfiguration : EntityWithUlidIdConfiguration<Scope>
{
    public override void Configure(EntityTypeBuilder<Scope> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(256);
    }
}
