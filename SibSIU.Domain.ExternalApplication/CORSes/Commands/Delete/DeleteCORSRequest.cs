using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Delete;
public sealed class DeleteCORSRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public string Creator { get; }

    public DeleteCORSRequest(Ulid id, string creator)
    {
        Id = id;
        Creator = creator;
    }

    public DeleteCORSRequest() : this(Ulid.Empty, string.Empty) { }

    public Error Validate()
    {
        if (Id == Ulid.Empty)
        {
            return CORSErrors.InvalidId;
        }

        if (string.IsNullOrWhiteSpace(Creator))
        {
            return CORSErrors.CreatorEmpty;
        }

        return Error.None;
    }
}
