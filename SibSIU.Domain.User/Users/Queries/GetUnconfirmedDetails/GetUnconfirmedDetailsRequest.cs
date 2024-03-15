using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedDetails;
public sealed class GetUnconfirmedDetailsRequest : BaseRequestById<Ulid, UnconfirmedUserDetails>, IValidated
{
    public GetUnconfirmedDetailsRequest(Ulid userId)
    {
        Id = userId;
    }

    public GetUnconfirmedDetailsRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UserErrors.InvalidUserId : Error.None;
    }
}
