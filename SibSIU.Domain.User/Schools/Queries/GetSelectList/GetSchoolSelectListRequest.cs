using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.Schools;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetSelectList;
public sealed class GetSchoolSelectListRequest : BaseRequestByFilter<SchoolItem>
{
    public GetSchoolSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetSchoolSelectListRequest() : this(string.Empty) { }
}
