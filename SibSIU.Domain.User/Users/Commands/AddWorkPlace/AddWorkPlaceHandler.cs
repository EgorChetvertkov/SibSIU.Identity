using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.AddWorkPlace;
public sealed class AddWorkPlaceHandler(
    ILogger<AddWorkPlaceHandler> logger,
    AuthContext auth) : IAddWorkPlaceHandler
{
    public async Task<Result<Message>> Handle(AddWorkPlaceRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(AddWorkPlaceRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        Unit? unit = await auth.Units.GetById(request.UnitId, cancellationToken);
        if (unit is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.OrganizationNotFound);
        }

        Post? post = await auth.Posts.GetById(request.PostId, cancellationToken);
        if (post is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(PostErrors.PostNotFound);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        WorkPlaces workPlace = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Unit = unit,
            Post = post,
            User = user,
        };

        await auth.WorkPlaces.AddAsync(workPlace, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Партнер добавлен"));
    }
}
