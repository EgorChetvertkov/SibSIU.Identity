using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class UnitConfiguration : EntityWithUlidIdConfiguration<Unit>
{
    public override void Configure(EntityTypeBuilder<Unit> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.FullName).IsRequired().HasMaxLength(512);
        builder.Property(u => u.ShortName).IsRequired().HasMaxLength(128);
        builder.Property(u => u.ParentId).HasConversion<UlidValueConverter>();
    }
}
