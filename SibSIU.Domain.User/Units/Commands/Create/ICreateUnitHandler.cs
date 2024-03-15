using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Units.Commands._Shared;

namespace SibSIU.Domain.UserManager.Units.Commands.Create;

public interface ICreateUnitHandler : IRequestHandler<CreateOrUpdateUnitRequest, Result<Message>>
{
}