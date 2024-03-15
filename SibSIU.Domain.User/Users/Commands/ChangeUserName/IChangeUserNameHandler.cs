using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;

public interface IChangeUserNameHandler : IRequestHandler<ChangeUserNameRequest, Result<Message>>
{
}