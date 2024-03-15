using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class AcademicFormConfiguration : EntityWithUlidIdConfiguration<AcademicForm>
{
    public override void Configure(EntityTypeBuilder<AcademicForm> builder)
    {
        base.Configure(builder);

        builder.Property(f => f.DeanCode).IsRequired();
        builder.Property(f => f.FullName).IsRequired().HasMaxLength(256);
        builder.Property(f => f.ShortName).IsRequired().HasMaxLength(128);

        builder.HasIndex(f => f.DeanCode)
            .HasDatabaseName("FormDeanCodeIndex").IsUnique();
    }
}
