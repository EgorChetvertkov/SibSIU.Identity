using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.UserData.Database.Entities;

public sealed class Gender : EntityWithUlidId
{
    public required string Name { get; set; }

    public ICollection<User> Users { get; set; } = [];
}