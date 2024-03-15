using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUserInfoByUserName;
public sealed class GetUserInfoByUserNameRequest : IRequest<Result<UserDetails>>, IValidated
{
    public string UserName { get; set; }

    public GetUserInfoByUserNameRequest(string userName)
    {
        UserName = userName;
    }

    public GetUserInfoByUserNameRequest() : this(string.Empty) { }

    public Error Validate()
    {
        return string.IsNullOrWhiteSpace(UserName) ?
            UserErrors.InvalidUserName :
            Error.None;
    }
}
