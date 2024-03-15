using SibSIU.Core.Database.EF.Entities;
using SibSIU.UserData.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.Auth.Database.Entities;
public sealed class UserRole : BaseEntity
{
    public Ulid UserId { get; set; }
    public Ulid RoleId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; } = null!;
}
