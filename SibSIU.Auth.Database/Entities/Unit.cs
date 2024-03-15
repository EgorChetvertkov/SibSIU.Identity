using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class Unit : EntityWithUlidId
{
    public required string FullName { get; set; } = null!;
    public required string ShortName { get; set; } = null!;
    public required int? DeanCode { get; set; }
    public Ulid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Unit? Parent { get; set; }
    public ICollection<Unit> Children { get; set; } = [];
    public ICollection<WorkPlaces> Employees { get; set; } = [];
    public InstituteUnit? InstituteUnit { get; set; }
    public DepartmentUnit? DepartmentUnit { get; set; }
}