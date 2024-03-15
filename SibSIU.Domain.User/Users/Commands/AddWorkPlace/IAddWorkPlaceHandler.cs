using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.AddWorkPlace;

public interface IAddWorkPlaceHandler : IRequestHandler<AddWorkPlaceRequest, Result<Message>>
{
}