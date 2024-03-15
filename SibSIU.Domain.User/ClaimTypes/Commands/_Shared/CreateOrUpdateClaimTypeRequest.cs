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

    public CreateOrUpdateClaimTypeRequest(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public CreateOrUpdateClaimTypeRequest() : this(Ulid.Empty, string.Empty) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return ClaimTypeErrors.ClaimTypeNameEmpty;
        }

        return Error.None;
    }
}
