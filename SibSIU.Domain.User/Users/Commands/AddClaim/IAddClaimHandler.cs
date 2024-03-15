using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.AddClaim;

public interface IAddClaimHandler : IRequestHandler<AddClaimRequest, Result<Message>>
{
}