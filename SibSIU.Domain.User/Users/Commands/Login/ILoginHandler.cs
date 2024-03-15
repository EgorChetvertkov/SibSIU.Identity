using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.Login;

public interface ILoginHandler : IRequestHandler<LoginRequest, Result<LoginResult>>
{
}