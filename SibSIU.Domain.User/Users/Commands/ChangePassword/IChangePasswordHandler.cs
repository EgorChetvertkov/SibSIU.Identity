using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePassword;

public interface IChangePasswordHandler : IRequestHandler<ChangePasswordRequest, Result<Message>>
{
}