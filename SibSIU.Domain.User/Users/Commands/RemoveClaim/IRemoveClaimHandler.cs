using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveClaim;

public interface IRemoveClaimHandler : IRequestHandler<RemoveClaimRequest, Result<Message>>
{
}