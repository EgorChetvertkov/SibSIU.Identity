using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Units.Commands.Delete;
public sealed class DeleteUnitRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public DeleteUnitRequest(Ulid schoolId)
    {
        Id = schoolId;
    }

    public DeleteUnitRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UnitErrors.InvalidUnitId : Error.None;
    }
}
