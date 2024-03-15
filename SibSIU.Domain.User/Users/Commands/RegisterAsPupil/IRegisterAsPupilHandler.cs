using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsPupil;

public interface IRegisterAsPupilHandler : IRequestHandler<RegisterAsPupilRequest, Result<Message>>
{
}