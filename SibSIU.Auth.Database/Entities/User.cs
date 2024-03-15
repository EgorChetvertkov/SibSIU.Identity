using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;
public sealed class User : EntityWithUlidId
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required bool EmailConfirmed { get; set; }
    public required string? PhoneNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string? Patronymic { get; set; }
    public required string Password { get; set; }
    public required string PasswordSalt { get; set; }
    public required bool IsTemporaryPassword { get; set; }
    public required DateTimeOffset? BirthOfDate { get; set; }
    public required bool IsConfirmedUser { get; set; }
    public Ulid? GenderId { get; set; }

    [ForeignKey(nameof(GenderId))]
    public Gender? Gender { get; set; }
    public ICollection<Partner> Partners { get; set; } = [];
    public ICollection<Pupil> Pupils { get; set; } = [];
    public ICollection<Student> Students { get; set; } = [];
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<WorkPlaces> WorkPlaces { get; set; } = [];
    public ICollection<AuthClaim> Claims { get; set; } = [];
}
