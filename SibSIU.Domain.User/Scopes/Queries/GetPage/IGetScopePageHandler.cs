using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetPage;

public interface IGetScopePageHandler : IRequestHandler<GetScopePageRequest, Result<PaginationList<ScopeRowItem>>>
{
}