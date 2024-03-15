using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class DirectionOfTraining : EntityWithUlidId
{
    public required int DeanCode { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public Ulid ImplementingChairId { get; set; }
    public Ulid DeveloperInstituteId { get; set; }

    [ForeignKey(nameof(ImplementingChairId))]
    public DepartmentUnit ImplementingChair { get; set; } = null!;
    [ForeignKey(nameof(DeveloperInstituteId))]
    public InstituteUnit DeveloperInstitute { get; set; } = null!;
    public ICollection<AcademicGroup> AcademicGroups { get; set; } = [];
}