using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class WorkPlacesConfiguration : EntityWithUlidIdConfiguration<WorkPlaces>
{
    public override void Configure(EntityTypeBuilder<WorkPlaces> builder)
    {
        base.Configure(builder);

        builder.Property(eu => eu.PostId).HasConversion<UlidValueConverter>();
        builder.Property(eu => eu.UserId).HasConversion<UlidValueConverter>();
        builder.Property(eu => eu.UnitId).HasConversion<UlidValueConverter>();

        builder.HasIndex(eu => new { eu.UserId, eu.UnitId, eu.PostId })
            .HasDatabaseName("UserWorkPlaceIndex").IsUnique();
    }
}
