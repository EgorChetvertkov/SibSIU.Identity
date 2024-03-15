using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Organizations;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetPage;

public interface IGetOrganizationPageHandler
    : IRequestHandler<GetOrganizationPageRequest, Result<PaginationList<OrganizationRowItem>>>
{
}