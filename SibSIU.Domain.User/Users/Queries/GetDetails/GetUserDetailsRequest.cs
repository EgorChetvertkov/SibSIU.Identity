using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetDetails;
public sealed class GetUserDetailsRequest : BaseRequestById<Ulid, UserDetails>
{
    public GetUserDetailsRequest(Ulid userId)
    {
        Id = userId;
    }

    public GetUserDetailsRequest() : this(Ulid.Empty) { }   
}
