using SibSIU.Identity.Models.ClaimTypes;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Identity.Models.User.Manage;
public sealed class ClaimDetails
{
    public Ulid ClaimId { get; set; }
    public List<ScopeItem> Scopes { get; set; }
    public ClaimTypeItem ClaimType { get; set; }
    public string Value { get; set; }

    public ClaimDetails(Ulid claimId, List<ScopeItem> scopes, ClaimTypeItem claimType, string value)
    {
        ClaimId = claimId;
        Scopes = scopes;
        ClaimType = claimType;
        Value = value;
    }

    public ClaimDetails() : this(Ulid.Empty, [], new(), string.Empty) { }
}
