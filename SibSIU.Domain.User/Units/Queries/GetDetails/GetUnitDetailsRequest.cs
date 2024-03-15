using SibSIU.Core.Services.RequestById;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Domain.UserManager.Units.Queries.GetDetails;
public sealed class GetUnitDetailsRequest : BaseRequestById<Ulid, UnitDetails>
{
    public GetUnitDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetUnitDetailsRequest() : this(Ulid.Empty) { }
}
