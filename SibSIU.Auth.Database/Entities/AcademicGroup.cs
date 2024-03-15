using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class AcademicGroup : EntityWithUlidId
{
    public required string Name { get; set; }
    public required int StartYear { get; set; }
    public Ulid AcademicLevelId { get; set; }
    public Ulid AcademicFormId { get; set; }
    public Ulid DirectionOfTrainingId { get; set; }
    public Ulid DirectorateInstituteId { get; set; }

    [ForeignKey(nameof(AcademicLevelId))]
    public AcademicLevel Level { get; set; } = null!;
    [ForeignKey(nameof(AcademicFormId))]
    public AcademicForm Form { get; set; } = null!;
    [ForeignKey(nameof(DirectionOfTrainingId))]
    public DirectionOfTraining DirectionOfTraining { get; set; } = null!;
    [ForeignKey(nameof(DirectorateInstituteId))]
    public InstituteUnit DirectorateInstitute { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = [];
}