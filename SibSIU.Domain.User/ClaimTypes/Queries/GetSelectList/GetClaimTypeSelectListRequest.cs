using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetSelectList;
public sealed class GetClaimTypeSelectListRequest : BaseRequestByFilter<ClaimTypeItem>
{
    public GetClaimTypeSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetClaimTypeSelectListRequest() : this(string.Empty) { }
}
