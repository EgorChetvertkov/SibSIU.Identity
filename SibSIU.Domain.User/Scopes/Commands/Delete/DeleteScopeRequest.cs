using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Scopes.Commands.Delete;
public sealed class DeleteScopeRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public DeleteScopeRequest(Ulid schoolId)
    {
        Id = schoolId;
    }

    public DeleteScopeRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? ScopeErrors.InvalidScopeId : Error.None;
    }
}
