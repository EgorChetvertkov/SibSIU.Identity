using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class DepartmentUnit
{
    public Ulid UnitId { get; set; }
    [ForeignKey(nameof(UnitId))]
    public Unit Unit { get; set; } = null!;
    public ICollection<DirectionOfTraining> ImplementedDirections { get; set; } = [];
}