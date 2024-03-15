using SibSIU.Core.Services.RequestById;
using SibSIU.Identity.Models.Organizations;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetDetails;
public sealed class GetOrganizationDetailsRequest
    : BaseRequestById<Ulid, OrganizationDetails>
{
    public GetOrganizationDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetOrganizationDetailsRequest() : this(Ulid.Empty) { }
}
