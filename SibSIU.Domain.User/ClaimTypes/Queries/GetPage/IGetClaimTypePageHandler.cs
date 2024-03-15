using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetPage;

public interface IGetClaimTypePageHandler : IRequestHandler<GetClaimTypePageRequest, Result<PaginationList<ClaimTypeRowItem>>>
{
}