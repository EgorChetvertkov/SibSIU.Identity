using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.DeleteByCreator;
public sealed class DeleteCORSByCreatorRequest : IRequest<Result<Message>>, IValidated
{
    public string Creator { get; }

    public DeleteCORSByCreatorRequest(string creator)
    {
        Creator = creator.TrimOrEmpty();
    }

    public DeleteCORSByCreatorRequest() : this(string.Empty) { }

    public Error Validate()
    {
        return string.IsNullOrWhiteSpace(Creator) ? CORSErrors.CreatorEmpty : Error.None;
    }
}
