using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ConfirmedEmail;

public interface IConfirmedEmailHandler : IRequestHandler<ConfirmedEmailRequest, Result<Message>>
{
}