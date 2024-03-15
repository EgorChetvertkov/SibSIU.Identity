using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.AddPartner;

public interface IAddPartnerHandler : IRequestHandler<AddPartnerRequest, Result<Message>>
{
}