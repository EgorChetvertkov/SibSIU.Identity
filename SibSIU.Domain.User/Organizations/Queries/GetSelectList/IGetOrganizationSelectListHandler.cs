using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.Organizations;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetSelectList;

public interface IGetOrganizationSelectListHandler
    : IRequestHandler<GetOrganizationSelectListRequest, List<OrganizationItem>>
{
}