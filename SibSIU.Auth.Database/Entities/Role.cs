using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.UserData.Database.Entities;
public sealed class Role : EntityWithUlidId
{
    public required string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
