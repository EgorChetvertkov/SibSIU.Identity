using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Scopes.Commands._Shared;

namespace SibSIU.Domain.UserManager.Scopes.Commands.Create;

public interface ICreateScopeHandler : IRequestHandler<CreateOrUpdateScopeRequest, Result<Message>>
{
}