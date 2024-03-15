using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RemovePupil;

public interface IRemovePupilHandler : IRequestHandler<RemovePupilRequest, Result<Message>>
{
}