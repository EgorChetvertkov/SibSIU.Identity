using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class PupilConfiguration : EntityWithUlidIdConfiguration<Pupil>
{
    public override void Configure(EntityTypeBuilder<Pupil> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ClassLitter).IsRequired();
        builder.Property(p => p.ClassNumber).IsRequired();
        builder.Property(p => p.SchoolId).IsRequired().HasConversion<UlidValueConverter>();
    }
}
