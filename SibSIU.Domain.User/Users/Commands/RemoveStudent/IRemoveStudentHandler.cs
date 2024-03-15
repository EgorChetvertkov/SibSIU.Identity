using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveStudent;

public interface IRemoveStudentHandler : IRequestHandler<RemoveStudentRequest, Result<Message>>
{
}