using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.ClaimTypes.Commands._Shared;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands.Create;

public interface ICreateClaimTypeHandler : IRequestHandler<CreateOrUpdateClaimTypeRequest, Result<Message>>
{
}