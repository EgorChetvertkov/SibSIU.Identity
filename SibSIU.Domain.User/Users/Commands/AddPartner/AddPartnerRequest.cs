using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.AddPartner;
public sealed class AddPartnerRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; set; }
    public Ulid OrganizationId { get; set; }
    public Ulid PostId { get; set; }

    public AddPartnerRequest(Ulid userId, Ulid organizationId, Ulid postId)
    {
        UserId = userId;
        OrganizationId = organizationId;
        PostId = postId;
    }

    public AddPartnerRequest() : this(Ulid.Empty, Ulid.Empty, Ulid.Empty) { }

    public Error Validate()
    {
        if (Ulid.Empty == UserId)
        {
            return UserErrors.InvalidUserId;
        }

        if (Ulid.Empty == OrganizationId)
        {
            return OrganizationErrors.InvalidOrganizationId;
        }

        if (Ulid.Empty == PostId)
        {
            return PostErrors.InvalidPostId;
        }

        return Error.None;
    }
}
