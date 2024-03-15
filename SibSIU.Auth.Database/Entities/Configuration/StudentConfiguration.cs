using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class StudentConfiguration : EntityWithUlidIdConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        builder.HasIndex(s => s.DeanCode)
            .HasDatabaseName("StudentDeanCodeIndex").IsUnique();

        builder.Property(s => s.DeanCode).IsRequired();
        builder.Property(s => s.Rank).IsRequired();

        builder.Property(s => s.UserId).HasConversion<UlidValueConverter>();
        builder.Property(s => s.AcademicGroupId).HasConversion<UlidValueConverter>();
    }
}
