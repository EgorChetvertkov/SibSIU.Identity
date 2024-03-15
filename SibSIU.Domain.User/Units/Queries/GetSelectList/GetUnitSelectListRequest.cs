using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Domain.UserManager.Units.Queries.GetSelectList;
public sealed class GetUnitSelectListRequest : BaseRequestByFilter<UnitItem>
{
    public GetUnitSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetUnitSelectListRequest() : this(string.Empty) { }
}
