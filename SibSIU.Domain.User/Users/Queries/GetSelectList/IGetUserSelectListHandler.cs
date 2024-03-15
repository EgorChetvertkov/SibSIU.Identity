using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetSelectList;

public interface IGetUserSelectListHandler : IRequestHandler<GetUserSelectListRequest, List<UserItem>>
{
}