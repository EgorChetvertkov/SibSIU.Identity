using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.UserData.Database.Entities;

public sealed class Post : EntityWithUlidId
{
    public required string Name { get; set; }

    public ICollection<WorkPlaces> EmployeeUnits { get; set; } = [];
    public ICollection<Partner> Partners { get; set; } = [];
}