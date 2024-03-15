using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class InstituteUnit
{
    public Ulid UnitId { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit Unit { get; set; } = null!;
    public ICollection<AcademicGroup> AcademicGroupsInDirectorate { get; set; } = [];
    public ICollection<DirectionOfTraining> DevelopDirectionOfTraining { get; set; } = [];
}