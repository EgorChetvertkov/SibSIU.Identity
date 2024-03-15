using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Units.Commands._Shared;

namespace SibSIU.Domain.UserManager.Units.Commands.Update;

public interface IUpdateUnitHandler : IRequestHandler<CreateOrUpdateUnitRequest, Result<Message>>
{
}