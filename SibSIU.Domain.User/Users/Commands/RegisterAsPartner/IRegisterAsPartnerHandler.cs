using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsPartner;

public interface IRegisterAsPartnerHandler : IRequestHandler<RegisterAsPartnerRequest, Result<Message>>
{
}