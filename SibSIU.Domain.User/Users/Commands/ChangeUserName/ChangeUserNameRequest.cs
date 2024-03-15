using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage.Update;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;
public sealed class ChangeUserNameRequest :
    IRequest<Result<Message>>,
    IRequest<Result<ChangeUserNameResult>>,
    IValidated
{
    public Ulid UserId { get; }
    public string UserName{ get; }

    public ChangeUserNameRequest(Ulid userId, ChangeUserNameData data)
    {
        UserId = userId;
        UserName = data.NewUserName.TrimOrEmpty();
    }

    public ChangeUserNameRequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (string.IsNullOrEmpty(UserName))
        {
            return UserErrors.InvalidUserName;
        }

        return Error.None;
    }
}
