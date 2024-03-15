using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class OrganizationConfiguration : EntityWithUlidIdConfiguration<Organization>
{
    public override void Configure(EntityTypeBuilder<Organization> builder)
    {
        base.Configure(builder);

        builder.HasIndex(o => o.OGRN)
            .HasDatabaseName("OrganizationOGRNIndex").IsUnique();
        builder.HasIndex(o => o.TIN)
            .HasDatabaseName("OrganizationTINIndex").IsUnique();

        builder.Property(o => o.FullName).IsRequired().HasMaxLength(512);
        builder.Property(o => o.ShortName).IsRequired().HasMaxLength(128);
        builder.Property(o => o.OGRN).IsRequired().HasMaxLength(13);
        builder.Property(o => o.TIN).IsRequired().HasMaxLength(10);
        builder.Property(o => o.KPP).IsRequired().HasMaxLength(9);
    }
}
