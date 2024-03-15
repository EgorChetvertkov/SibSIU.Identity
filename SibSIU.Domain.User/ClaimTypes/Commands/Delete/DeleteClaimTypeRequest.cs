using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands.Delete;
public sealed class DeleteClaimTypeRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public DeleteClaimTypeRequest(Ulid schoolId)
    {
        Id = schoolId;
    }

    public DeleteClaimTypeRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? ClaimTypeErrors.InvalidClaimTypeId : Error.None;
    }
}
