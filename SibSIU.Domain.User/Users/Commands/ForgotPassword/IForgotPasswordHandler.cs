using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ForgotPassword;

public interface IForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, Result<Message>>
{
}