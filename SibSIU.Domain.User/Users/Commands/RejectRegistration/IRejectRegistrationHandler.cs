using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RejectRegistration;

public interface IRejectRegistrationHandler : IRequestHandler<RejectRegistrationRequest, Result<Message>>
{
}