using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.AcademicGroups;

namespace SibSIU.Domain.UserManager.Groups.Queries.GetSelectList;
public sealed class GetGroupSelectListRequest : BaseRequestByFilter<AcademicGroupItem>
{
    public GetGroupSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetGroupSelectListRequest() : this(string.Empty) { }
}
