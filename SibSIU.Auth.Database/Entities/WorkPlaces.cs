using SibSIU.Core.Database.EF.Entities;
using SibSIU.UserData.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.Auth.Database.Entities;

public sealed class WorkPlaces : EntityWithUlidId
{
    public Ulid UserId { get; set; }
    public Ulid UnitId { get; set; }
    public Ulid PostId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(UnitId))]
    public Unit Unit { get; set; } = null!;
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; } = null!;
}