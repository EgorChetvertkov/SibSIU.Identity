using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Scopes.Commands._Shared;

namespace SibSIU.Domain.UserManager.Scopes.Commands.Update;

public interface IUpdateScopeHandler : IRequestHandler<CreateOrUpdateScopeRequest, Result<Message>>
{
}