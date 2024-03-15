using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ResetPassword;

public interface IResetPasswordHandler : IRequestHandler<ResetPasswordRequest, Result<Message>>
{
}