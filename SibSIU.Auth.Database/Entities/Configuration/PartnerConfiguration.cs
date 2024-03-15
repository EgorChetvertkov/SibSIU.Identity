using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class PartnerConfiguration : EntityWithUlidIdConfiguration<Partner>
{
    public override void Configure(EntityTypeBuilder<Partner> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.PostId).IsRequired().HasConversion<UlidValueConverter>();
        builder.Property(p => p.OrganizationId).IsRequired().HasConversion<UlidValueConverter>();
    }
}
