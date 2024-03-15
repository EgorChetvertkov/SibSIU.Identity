using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.AddClaim;
public sealed class AddClaimRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; }
    public Ulid ClaimTypeId { get; }
    public Ulid ScopeId { get; }
    public string Value { get; }

    public AddClaimRequest(Ulid userId, Ulid claimTypeId, Ulid scopeId, string value)
    {
        UserId = userId;
        ClaimTypeId = claimTypeId;
        ScopeId = scopeId;
        Value = value.TrimOrEmpty();
    }

    public AddClaimRequest() : this(Ulid.Empty, Ulid.Empty, Ulid.Empty, string.Empty) { }

    public Error Validate()
    {
        if (ClaimTypeId == Ulid.Empty)
        {
            return ClaimTypeErrors.InvalidClaimTypeId;
        }

        if (ScopeId == Ulid.Empty)
        {
            return ScopeErrors.InvalidScopeId;
        }

        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (string.IsNullOrWhiteSpace(Value))
        {
            return ClaimErrors.ClaimValueEmpty;
        }

        return Error.None;
    }
}
