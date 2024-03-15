using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetSelectList;
public sealed class GetScopeSelectListRequest : BaseRequestByFilter<ScopeItem>
{
    public GetScopeSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetScopeSelectListRequest() : this(string.Empty) { }
}
