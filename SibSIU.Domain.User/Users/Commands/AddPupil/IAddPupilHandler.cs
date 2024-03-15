using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.AddPupil;

public interface IAddPupilHandler : IRequestHandler<AddPupilRequest, Result<Message>>
{
}