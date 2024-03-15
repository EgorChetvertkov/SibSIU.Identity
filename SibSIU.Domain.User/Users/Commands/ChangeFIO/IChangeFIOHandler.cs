using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeFIO;

public interface IChangeFIOHandler : IRequestHandler<ChangeFIORequest, Result<Message>>
{
}