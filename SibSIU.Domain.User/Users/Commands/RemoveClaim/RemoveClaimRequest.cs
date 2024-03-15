using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveClaim;
public sealed class RemoveClaimRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public RemoveClaimRequest(Ulid partnerId)
    {
        Id = partnerId;
    }

    public RemoveClaimRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UserErrors.InvalidUserId : Error.None;
    }
}
