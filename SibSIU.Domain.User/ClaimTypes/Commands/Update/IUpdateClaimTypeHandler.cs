using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.ClaimTypes.Commands._Shared;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands.Update;

public interface IUpdateClaimTypeHandler : IRequestHandler<CreateOrUpdateClaimTypeRequest, Result<Message>>
{
}