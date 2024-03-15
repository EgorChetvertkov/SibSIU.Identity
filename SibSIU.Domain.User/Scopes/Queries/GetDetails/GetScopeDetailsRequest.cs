using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetDetails;
public sealed class GetScopeDetailsRequest : BaseRequestById<Ulid, ScopeDetails>
{
    public GetScopeDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetScopeDetailsRequest() : this(Ulid.Empty) { }
}
