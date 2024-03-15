using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.AddStudent;

public interface IAddStudentHandler : IRequestHandler<AddStudentRequest, Result<Message>>
{
}