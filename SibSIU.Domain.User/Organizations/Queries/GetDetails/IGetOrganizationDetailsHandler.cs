using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Organizations;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetDetails;
public interface IGetOrganizationDetailsHandler : IRequestHandler<GetOrganizationDetailsRequest, Result<OrganizationDetails>>
{
}
