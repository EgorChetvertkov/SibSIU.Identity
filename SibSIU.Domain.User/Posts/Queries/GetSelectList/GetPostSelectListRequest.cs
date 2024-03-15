using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetSelectList;
public sealed class GetPostSelectListRequest : BaseRequestByFilter<PostItem>
{
    public GetPostSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetPostSelectListRequest() : this(string.Empty) { }
}
