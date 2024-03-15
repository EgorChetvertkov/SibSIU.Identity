using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveStudent;
public sealed class RemoveStudentRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public RemoveStudentRequest(Ulid studentId)
    {
        Id = studentId;
    }

    public RemoveStudentRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? StudentErrors.InvalidStudentId : Error.None;
    }
}
