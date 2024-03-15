using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;

public interface ISubmitRegistrationHandler : IRequestHandler<SubmitRegistrationRequest, Result<Message>>
{
}