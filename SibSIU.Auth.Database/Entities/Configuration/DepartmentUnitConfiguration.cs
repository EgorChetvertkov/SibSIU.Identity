using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class DepartmentUnitConfiguration : IEntityTypeConfiguration<DepartmentUnit>
{
    public void Configure(EntityTypeBuilder<DepartmentUnit> builder)
    {
        builder.HasKey(u => u.UnitId);
        builder.Property(u => u.UnitId).HasConversion<UlidValueConverter>();
    }
}
