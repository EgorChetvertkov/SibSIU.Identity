using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Scopes.Commands.Delete;

public interface IDeleteScopeHandler : IRequestHandler<DeleteScopeRequest, Result<Message>>
{
}