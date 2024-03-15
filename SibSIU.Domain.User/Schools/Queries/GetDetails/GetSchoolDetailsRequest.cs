using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Schools;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetDetails;
public sealed class GetSchoolDetailsRequest : BaseRequestById<Ulid, SchoolDetails>
{
    public GetSchoolDetailsRequest(Ulid id)
    {
        Id = id;
    }

    public GetSchoolDetailsRequest() : this(Ulid.Empty) { }
}
