using SibSIU.Identity.Models.ClaimTypes;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Identity.Models.User.Manage;
public sealed class ClaimDetails
{
    public Ulid ClaimId { get; set; }
    public ScopeItem Scope { get; set; }
    public ClaimTypeItem ClaimType { get; set; }
    public string Value { get; set; }

    public ClaimDetails(Ulid claimId, ScopeItem scope, ClaimTypeItem claimType, string value)
    {
        ClaimId = claimId;
        Scope = scope;
        ClaimType = claimType;
        Value = value;
    }

    public ClaimDetails() : this(Ulid.Empty, new(), new(), string.Empty) { }
}
