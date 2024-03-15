using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Organizations.Commands._Shared;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Update;

public interface IUpdateOrganizationHandler
    : IRequestHandler<CreateOrUpdateOrganizationRequest, Result<Message>>
{
}