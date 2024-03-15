using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.Core.Database.EF.Converters;
using SibSIU.Core.Database.EF.Entities.Configurations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class UserConfiguration : EntityWithUlidIdConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasIndex(u => u.UserName)
            .HasDatabaseName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.Email)
            .HasDatabaseName("EmailIndex").IsUnique();

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.EmailConfirmed).IsRequired();
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(128);
        builder.Property(u => u.Patronymic).HasMaxLength(128);
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.IsConfirmedUser).IsRequired();
        builder.Property(u => u.BirthOfDate).HasConversion<DateTimeOffsetValueConverter>();

        builder.Property(u => u.GenderId).HasConversion<UlidValueConverter>();

        builder.HasQueryFilter(u => u.IsActive && u.IsConfirmedUser);
    }
}
