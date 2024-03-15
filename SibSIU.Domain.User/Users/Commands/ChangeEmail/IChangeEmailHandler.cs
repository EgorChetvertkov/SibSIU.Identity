using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;

public interface IChangeEmailHandler : IRequestHandler<ChangeEmailRequest, Result<Message>>
{
}