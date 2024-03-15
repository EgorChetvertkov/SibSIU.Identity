using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class RoleConfiguration : EntityWithUlidIdConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.HasIndex(r => r.Name).
            HasDatabaseName("RoleNameIndex").IsUnique();

        builder.Property(r => r.Name).IsRequired().HasMaxLength(256);
    }
}
