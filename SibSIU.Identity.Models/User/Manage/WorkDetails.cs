using SibSIU.Identity.Models.Posts;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Identity.Models.User.Manage;

public sealed class WorkDetails
{
    public Ulid WorkPlaceId { get; set; }
    public UnitItem Unit { get; set; }
    public PostItem Post { get; set; }

    public WorkDetails(Ulid workPlaceId, UnitItem unit, PostItem post)
    {
        WorkPlaceId = workPlaceId;
        Unit = unit;
        Post = post;
    }

    public WorkDetails() : this(Ulid.Empty, new(), new()) { }
}