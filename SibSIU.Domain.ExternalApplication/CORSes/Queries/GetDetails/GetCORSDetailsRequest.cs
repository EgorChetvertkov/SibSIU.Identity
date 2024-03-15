using SibSIU.Core.Services.RequestById;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetDetails;
public sealed class GetCORSDetailsRequest
    : BaseRequestById<Ulid, CORSDetails>
{
    public GetCORSDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetCORSDetailsRequest() : this(Ulid.Empty) { }
}
