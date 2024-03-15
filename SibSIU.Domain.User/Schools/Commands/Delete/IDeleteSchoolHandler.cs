using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Schools.Commands.Delete;

public interface IDeleteSchoolHandler : IRequestHandler<DeleteSchoolRequest, Result<Message>>
{
}