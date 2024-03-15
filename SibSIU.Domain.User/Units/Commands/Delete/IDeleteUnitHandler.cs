using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Units.Commands.Delete;

public interface IDeleteUnitHandler : IRequestHandler<DeleteUnitRequest, Result<Message>>
{
}