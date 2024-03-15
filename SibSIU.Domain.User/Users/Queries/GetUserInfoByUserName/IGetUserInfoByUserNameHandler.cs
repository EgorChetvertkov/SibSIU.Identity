using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUserInfoByUserName;
public interface IGetUserInfoByUserNameHandler : IRequestHandler<GetUserInfoByUserNameRequest, Result<UserDetails>>
{
}
