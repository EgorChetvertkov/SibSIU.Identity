using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.RemovePartner;
public sealed class RemovePartnerRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public RemovePartnerRequest(Ulid partnerId)
    {
        Id = partnerId;
    }

    public RemovePartnerRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UserErrors.InvalidUserId : Error.None;
    }
}
