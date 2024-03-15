using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class DirectionOfTrainingConfiguration : EntityWithUlidIdConfiguration<DirectionOfTraining>
{
    public override void Configure(EntityTypeBuilder<DirectionOfTraining> builder)
    {
        base.Configure(builder);

        builder.HasIndex(dot => dot.DeanCode)
            .HasDatabaseName("DotDeanCodeIndex").IsUnique();

        builder.Property(dot => dot.Name).IsRequired().HasMaxLength(256);
        builder.Property(dot => dot.Code).IsRequired().HasMaxLength(16);
        builder.Property(dot => dot.DeveloperInstituteId).HasConversion<UlidValueConverter>();
        builder.Property(dot => dot.ImplementingChairId).HasConversion<UlidValueConverter>();
    }
}
