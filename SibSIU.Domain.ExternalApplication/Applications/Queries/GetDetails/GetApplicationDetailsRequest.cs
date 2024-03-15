using SibSIU.Core.Services.RequestById;
using SibSIU.Identity.Models.Applications;

namespace SibSIU.Domain.ExternalApplication.Applications.Queries.GetDetails;
public sealed class GetApplicationDetailsRequest : BaseRequestById<ValueString, ApplicationDetails>
{
    public GetApplicationDetailsRequest(string id)
    {
        Id = new(id);
    }

    public GetApplicationDetailsRequest() : this(string.Empty) { }
}
