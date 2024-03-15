using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class SchoolConfiguration : EntityWithUlidIdConfiguration<School>
{
    public override void Configure(EntityTypeBuilder<School> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.FullName).IsRequired().HasMaxLength(512);
        builder.Property(s => s.ShortName).IsRequired().HasMaxLength(128);
    }
}
