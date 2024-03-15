using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.CreateUser;

public interface ICreateUserHandler : IRequestHandler<CreateUserRequest, Result<Message>>
{
}