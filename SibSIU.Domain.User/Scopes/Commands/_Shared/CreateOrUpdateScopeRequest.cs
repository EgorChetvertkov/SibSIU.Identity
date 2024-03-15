using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Scopes.Commands._Shared;
public sealed class CreateOrUpdateScopeRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid Id { get; }
    public string Name { get; }

    public CreateOrUpdateScopeRequest(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public CreateOrUpdateScopeRequest() : this(Ulid.Empty, string.Empty) { }

    public Error Validate()
    {
        return string.IsNullOrWhiteSpace(Name) ? ScopeErrors.ScopeNameEmpty : Error.None;
    }
}
