using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;

namespace SibSIU.CORS.Database.Entities.Configurations;
public sealed class AllowOriginConfigurations : EntityWithUlidIdConfiguration<AllowOrigin>
{
    public override void Configure(EntityTypeBuilder<AllowOrigin> builder)
    {
        base.Configure(builder);

        builder.Property(o => o.Creator).HasMaxLength(256).IsRequired();
        builder.Property(o => o.Origin).HasMaxLength(1024).IsRequired();
    }
}
