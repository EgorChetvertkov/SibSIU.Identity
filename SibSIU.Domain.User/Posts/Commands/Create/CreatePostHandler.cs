using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Posts.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Posts.Commands.Create;
public sealed class CreatePostHandler(
    ILogger<CreatePostHandler> logger,
    AuthContext auth) : ICreatePostHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdatePostRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(CreateOrUpdatePostRequest request, CancellationToken cancellationToken)
    {
        bool postAlreadyExists = await auth.Posts
            .Where(p => p.Name.ToLower() == request.Name.ToLower())
            .AnyAsync(cancellationToken);
        if (postAlreadyExists)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(PostErrors.PostAlreadyExists);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Post post = new()
        {
            CreateAt = now,
            UpdateAt = now,
            Id = Ulid.NewUlid(now),
            IsActive = true,
            Name = request.Name,
        };

        await auth.Posts.AddAsync(post, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Должность успешно создана"));
    }
}
