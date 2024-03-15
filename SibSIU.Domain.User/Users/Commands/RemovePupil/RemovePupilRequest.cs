using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.RemovePupil;
public sealed class RemovePupilRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public RemovePupilRequest(Ulid pupilId)
    {
        Id = pupilId;
    }

    public RemovePupilRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UserErrors.InvalidUserId : Error.None;
    }
}
