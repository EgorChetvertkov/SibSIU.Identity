using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetSelectList;

public interface IGetScopeSelectListHandler : IRequestHandler<GetScopeSelectListRequest, List<ScopeItem>>
{
}