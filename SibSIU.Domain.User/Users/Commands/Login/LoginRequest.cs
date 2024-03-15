using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.Login;
public class LoginRequest : IRequest<Result<LoginResult>>, IValidated
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public LoginRequest(string userName, string password)
    {
        UserName = userName.TrimOrEmpty();
        Password = password.TrimOrEmpty();
    }

    public LoginRequest() : this(string.Empty, string.Empty) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            return UserErrors.InvalidUserName;
        }

        if (Password?.Length < 8)
        {
            return UserErrors.PasswordAreShort;
        }

        return Error.None;
    }
}
