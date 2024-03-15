using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Delete;

public interface IDeleteOrganizationHandler : IRequestHandler<DeleteOrganizationRequest, Result<Message>>
{
}