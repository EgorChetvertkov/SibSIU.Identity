using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Create;
public sealed class CreateCORSRequest : IRequest<Result<Message>>, IValidated
{
    public string Creator { get; }
    public IReadOnlyList<string> Origins { get; }

    public CreateCORSRequest(string creator, List<string> origins)
    {
        Creator = creator.TrimOrEmpty();
        Origins = origins.Select(o => o.TrimOrEmpty()).ToList();
    }

    public CreateCORSRequest() : this(string.Empty, []) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(Creator))
        {
            return CORSErrors.CreatorEmpty;
        }

        if (Origins.Any(o => string.IsNullOrWhiteSpace(o)))
        {
            return CORSErrors.OriginEmpty;
        }

        return Error.None;
    }
}
