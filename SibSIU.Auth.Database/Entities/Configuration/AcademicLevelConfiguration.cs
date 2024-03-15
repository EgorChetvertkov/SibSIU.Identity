using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class AcademicLevelConfiguration : EntityWithUlidIdConfiguration<AcademicLevel>
{
    public override void Configure(EntityTypeBuilder<AcademicLevel> builder)
    {
        base.Configure(builder);

        builder.Property(l => l.DeanCode).IsRequired();
        builder.Property(l => l.FullName).IsRequired().HasMaxLength(256);
        builder.Property(l => l.ShortName).IsRequired().HasMaxLength(128);

        builder.HasIndex(l => l.DeanCode)
            .HasDatabaseName("LevelDeanCodeIndex").IsUnique();
    }
}
