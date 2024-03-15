using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.Organizations;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetSelectList;
public sealed class GetOrganizationSelectListRequest : BaseRequestByFilter<OrganizationItem>
{
    public GetOrganizationSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetOrganizationSelectListRequest() : this(string.Empty) { }
}
