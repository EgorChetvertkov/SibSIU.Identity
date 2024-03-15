using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Posts.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Posts.Commands.Update;
public sealed class UpdatePostHandler(
    ILogger<UpdatePostHandler> logger,
    AuthContext auth) : IUpdatePostHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdatePostRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(CreateOrUpdatePostRequest request, CancellationToken cancellationToken)
    {
        Post? post = await auth.Posts
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (post is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(PostErrors.PostNotFound);
        }

        bool postAlreadyExists = await auth.Posts
            .Where(p => p.Name.ToLower() == request.Name.ToLower() && p.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (postAlreadyExists)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(PostErrors.PostAlreadyExists);
        }

        post.Name = request.Name;
        post.UpdateAt = DateTimeOffset.UtcNow;

        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Должность успешно изменена"));
    }
}
