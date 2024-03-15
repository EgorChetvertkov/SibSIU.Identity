using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.UserId).HasConversion<UlidValueConverter>();
        builder.Property(ur => ur.RoleId).HasConversion<UlidValueConverter>();
    }
}
