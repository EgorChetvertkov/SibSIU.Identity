using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands._Shared;
public sealed class CreateOrUpdateClaimTypeRequest :
    IRequest<Result<Message>>,
    IValidated
{
    public Ulid Id { get; set; }
    public string Name { get; set; }
    public bool IncludeInAccessToken { get; set; }
    public bool IncludeInIdentityToken { get; set; }

    public CreateOrUpdateClaimTypeRequest(Ulid id, string name, bool includeInAccessToken, bool includeInIdentityToken)
    {
        Id = id;
        Name = name;
        IncludeInAccessToken = includeInAccessToken;
        IncludeInIdentityToken = includeInIdentityToken;
    }

    public CreateOrUpdateClaimTypeRequest() : this(Ulid.Empty, string.Empty, false, false) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return ClaimTypeErrors.ClaimTypeNameEmpty;
        }

        return Error.None;
    }
}
