using SibSIU.Core.Database.EF.Entities;

using System.ComponentModel.DataAnnotations.Schema;

namespace SibSIU.UserData.Database.Entities;

public sealed class Partner : EntityWithUlidId
{
    public Ulid UserId { get; set; }
    public Ulid OrganizationId { get; set; }
    public Ulid PostId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; } = null!;
    [ForeignKey(nameof(OrganizationId))]
    public Organization Organization { get; set; } = null!;
}