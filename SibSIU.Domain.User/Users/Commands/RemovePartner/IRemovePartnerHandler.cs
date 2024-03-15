using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RemovePartner;

public interface IRemovePartnerHandler : IRequestHandler<RemovePartnerRequest, Result<Message>>
{
}