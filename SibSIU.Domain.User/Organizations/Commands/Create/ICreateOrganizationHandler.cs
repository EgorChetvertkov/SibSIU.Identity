using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Organizations.Commands._Shared;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Create;

public interface ICreateOrganizationHandler
    : IRequestHandler<CreateOrUpdateOrganizationRequest, Result<Message>>
{
}