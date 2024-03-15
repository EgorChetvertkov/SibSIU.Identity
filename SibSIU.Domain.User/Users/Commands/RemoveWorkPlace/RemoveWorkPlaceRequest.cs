using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveWorkPlace;
public sealed class RemoveWorkPlaceRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public RemoveWorkPlaceRequest(Ulid workPlaceId)
    {
        Id = workPlaceId;
    }

    public RemoveWorkPlaceRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UserErrors.InvalidUserId : Error.None;
    }
}
