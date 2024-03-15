using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.UserData.Database.Entities;

public sealed class School : EntityWithUlidId
{
    public required string FullName { get; set; } = null!;
    public required string ShortName { get; set; } = null!;

    public ICollection<Pupil> Pupils { get; set; } = [];
}