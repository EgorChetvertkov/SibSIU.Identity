using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.Names;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database;
internal static class DataSeeder
{
    internal static void Seed(this ModelBuilder builder)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        Role admin = new()
        {
            Id = Ulid.NewUlid(now),
            Name = RoleNames.BaseAdministrator,
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
        };

        builder.Entity<Role>().HasData(admin);

        Gender male = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Name = "Мужской",
        };
        Gender female = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Name = "Женский",
        };

        builder.Entity<Gender>().HasData(male, female);

        var result = HashCalculator.Hash("adminDDT_1");
        User userAdmin = new()
        {
            Id = Ulid.NewUlid(),
            Email = "admin@sibsiu.ru",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "Admin",
            Patronymic = "Admin",
            UserName = "Admin",
            BirthOfDate = new DateTimeOffset(2001, 1, 18, 0, 0, 0, TimeSpan.Zero),
            GenderId = male.Id,
            PhoneNumber = "+7-(900)-00-00-0000",
            Password = result.Password,
            PasswordSalt = result.Salt,
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            IsConfirmedUser = true,
            IsTemporaryPassword = false,
        };

        builder.Entity<User>().HasData(userAdmin);

        UserRole userAdminWithAdminRole = new()
        {
            RoleId = admin.Id,
            UserId = userAdmin.Id,
        };

        builder.Entity<UserRole>().HasData(userAdminWithAdminRole);

        Organization organization = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            ShortName = "СибГИУ",
            FullName = "Федеральное государственное бюджетное образовательное учреждение высшего образования «Сибирский государственный индустриальный университет»",
            KPP = "421701001",
            OGRN = "1024201470908",
            TIN = "4216003509"
        };

        builder.Entity<Organization>().HasData(organization);

        Unit sibSIU = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            DeanCode = null,
            Parent = null,
            ShortName = "СибГИУ",
            FullName = "Федеральное государственное бюджетное образовательное учреждение высшего образования «Сибирский государственный индустриальный университет»",
        };

        builder.Entity<Unit>().HasData(sibSIU);
    }
}
