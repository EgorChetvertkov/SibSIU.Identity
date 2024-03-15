using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePhone;

public interface IChangePhoneHandler : IRequestHandler<ChangePhoneRequest, Result<Message>>
{
}