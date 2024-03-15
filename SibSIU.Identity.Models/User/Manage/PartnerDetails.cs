using SibSIU.Identity.Models.Organizations;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Identity.Models.User.Manage;

public sealed class PartnerDetails
{
    public Ulid PartnerId { get; set; }
    public OrganizationItem Organization { get; set; }
    public PostItem Post { get; set; }

    public PartnerDetails(Ulid partnerId, OrganizationItem organization, PostItem post)
    {
        PartnerId = partnerId;
        Organization = organization;
        Post = post;
    }

    public PartnerDetails() : this(Ulid.Empty, new(), new()) { }
}