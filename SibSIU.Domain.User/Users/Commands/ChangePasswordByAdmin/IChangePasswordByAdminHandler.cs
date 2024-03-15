using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePasswordByAdmin;

public interface IChangePasswordByAdminHandler : IRequestHandler<ChangePasswordByAdminRequest, Result<Message>>
{
}