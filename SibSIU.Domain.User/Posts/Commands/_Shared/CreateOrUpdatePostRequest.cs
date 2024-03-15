using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Posts.Commands._Shared;
public sealed class CreateOrUpdatePostRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid Id { get; }
    public string Name { get; }

    public CreateOrUpdatePostRequest(Ulid id, string name)
    {
        Id = id;
        Name = name.TrimOrEmpty();
    }

    public CreateOrUpdatePostRequest() : this(Ulid.Empty, string.Empty) { }

    public Error Validate()
    {
        return string.IsNullOrWhiteSpace(Name) ? PostErrors.InvalidPostName : Error.None;
    }
}
