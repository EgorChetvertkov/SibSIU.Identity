using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsStudent;

public interface IRegisterAsStudentHandler : IRequestHandler<RegisterAsStudentRequest, Result<Message>>
{
}