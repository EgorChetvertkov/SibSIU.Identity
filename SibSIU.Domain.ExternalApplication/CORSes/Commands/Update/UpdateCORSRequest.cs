using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Update;
public sealed class UpdateCORSRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid Id { get; }
    public string Origin { get; }
    public string Creator { get; }

    public UpdateCORSRequest(Ulid id,  string origin, string creator)
    {
        Id = id;
        Origin = origin;
        Creator = creator;
    }

    public UpdateCORSRequest() : this(Ulid.Empty, string.Empty, string.Empty) { }

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

        if (string.IsNullOrWhiteSpace(Origin))
        {
            return CORSErrors.OriginEmpty;
        }

        return Error.None;
    }
}
