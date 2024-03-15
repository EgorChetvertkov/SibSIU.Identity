using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class InstituteUnitConfiguration : IEntityTypeConfiguration<InstituteUnit>
{
    public void Configure(EntityTypeBuilder<InstituteUnit> builder)
    {
        builder.HasKey(i => i.UnitId);

        builder.Property(i => i.UnitId).HasConversion<UlidValueConverter>();
    }
}
