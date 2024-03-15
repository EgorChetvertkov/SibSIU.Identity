using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetDetails;
public sealed class GetPostDetailsRequest
    : BaseRequestById<Ulid, PostDetails>
{
    public GetPostDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetPostDetailsRequest() : this(Ulid.Empty) { }
}
