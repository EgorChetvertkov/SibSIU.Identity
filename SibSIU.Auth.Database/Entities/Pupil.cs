using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class Pupil : EntityWithUlidId
{
    public Ulid UserId { get; set; }
    public required int ClassNumber { get; set; }
    public required char ClassLitter { get; set; }
    public Ulid SchoolId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(SchoolId))]
    public School School { get; set; } = null!;
}