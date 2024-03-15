using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Schools.Commands.Delete;
public sealed class DeleteSchoolRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public DeleteSchoolRequest(Ulid schoolId)
    {
        Id = schoolId;
    }

    public DeleteSchoolRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? PostErrors.InvalidPostId : Error.None;
    }
}
