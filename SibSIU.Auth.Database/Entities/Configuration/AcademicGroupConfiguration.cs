using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class AcademicGroupConfiguration : EntityWithUlidIdConfiguration<AcademicGroup>
{
    public override void Configure(EntityTypeBuilder<AcademicGroup> builder)
    {
        base.Configure(builder);

        builder.Property(g => g.Name).IsRequired().HasMaxLength(16);
        builder.Property(g => g.StartYear).IsRequired();

        builder.HasIndex(g => g.Name)
            .HasDatabaseName("GroupNameIndex").IsUnique();

        builder.Property(g => g.AcademicLevelId).IsRequired().HasConversion<UlidValueConverter>();
        builder.Property(g => g.AcademicFormId).IsRequired().HasConversion<UlidValueConverter>();
        builder.Property(g => g.DirectorateInstituteId).IsRequired().HasConversion<UlidValueConverter>();
        builder.Property(g => g.DirectionOfTrainingId).IsRequired().HasConversion<UlidValueConverter>();
    }
}
