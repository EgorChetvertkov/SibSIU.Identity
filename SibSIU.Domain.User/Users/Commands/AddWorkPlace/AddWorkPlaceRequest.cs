using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.AddWorkPlace;
public sealed class AddWorkPlaceRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; set; }
    public Ulid UnitId { get; set; }
    public Ulid PostId { get; set; }

    public AddWorkPlaceRequest(Ulid userId, Ulid unitId, Ulid postId)
    {
        UserId = userId;
        UnitId = unitId;
        PostId = postId;
    }

    public AddWorkPlaceRequest() : this(Ulid.Empty, Ulid.Empty, Ulid.Empty) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (UnitId == Ulid.Empty)
        {
            return UnitErrors.InvalidUnitId;
        }

        if (PostId == Ulid.Empty)
        {
            return PostErrors.InvalidPostId;
        }

        return Error.None;
    }
}
