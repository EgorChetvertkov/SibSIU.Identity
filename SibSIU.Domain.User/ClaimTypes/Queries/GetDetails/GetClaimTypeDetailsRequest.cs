using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetDetails;
public sealed class GetClaimTypeDetailsRequest : BaseRequestById<Ulid, ClaimTypeDetails>
{
    public GetClaimTypeDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetClaimTypeDetailsRequest() : this(Ulid.Empty) { }
}
