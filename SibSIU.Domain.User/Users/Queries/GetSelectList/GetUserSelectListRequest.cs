using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetSelectList;
public sealed class GetUserSelectListRequest : BaseRequestByFilter<UserItem>
{
    public GetUserSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetUserSelectListRequest() : this(string.Empty) { }
}
