using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Delete;
public sealed class DeleteOrganizationRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public DeleteOrganizationRequest(Ulid id)
    {
        Id = id;
    }

    public DeleteOrganizationRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? OrganizationErrors.InvalidOrganizationId : Error.None;
    }
}
