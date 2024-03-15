using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class Student : EntityWithUlidId
{
    public Ulid UserId { get; set; }
    public required int DeanCode { get; set; }
    public required double Rank { get; set; }
    public Ulid AcademicGroupId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(AcademicGroupId))]
    public AcademicGroup Group { get; set; } = null!;
}