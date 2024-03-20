using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands._Shared;
public sealed class CreateOrUpdateClaimTypeRequest :
    IRequest<Result<Message>>,
    IValidated
{
    public Ulid Id { get; init; }
    public string Name { get; init; }
    public bool IncludeInAccessToken { get; init; }
    public bool IncludeInIdentityToken { get; init; }
    public List<Ulid> Scopes { get; init; }

    public CreateOrUpdateClaimTypeRequest(
        Ulid id,
        string name,
        bool includeInAccessToken,
        bool includeInIdentityToken,
        List<Ulid> scopes)
    {
        Id = id;
        Name = name;
        IncludeInAccessToken = includeInAccessToken;
        IncludeInIdentityToken = includeInIdentityToken;
        Scopes = scopes;
    }

    public CreateOrUpdateClaimTypeRequest() : this(Ulid.Empty, string.Empty, false, false, []) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return ClaimTypeErrors.ClaimTypeNameEmpty;
        }

        return Error.None;
    }
}
