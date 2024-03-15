using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.UserData.Database.Entities;

public sealed class AcademicLevel : EntityWithUlidId
{
    public required string FullName { get; set; }
    public required string ShortName { get; set; }
    public required int DeanCode { get; set; }

    public ICollection<AcademicGroup> Groups { get; set; } = [];
}