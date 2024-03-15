using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class PostConfiguration : EntityWithUlidIdConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.HasIndex(p => p.Name)
            .HasDatabaseName("PostNameIndex").IsUnique();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
    }
}
