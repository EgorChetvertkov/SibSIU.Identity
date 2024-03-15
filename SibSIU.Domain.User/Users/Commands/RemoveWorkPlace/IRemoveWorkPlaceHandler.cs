using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveWorkPlace;

public interface IRemoveWorkPlaceHandler : IRequestHandler<RemoveWorkPlaceRequest, Result<Message>>
{
}