using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Posts.Commands.Delete;
public sealed class DeletePostHandler(
    ILogger<DeletePostHandler> logger,
    AuthContext auth) : IDeletePostHandler
{
    public async Task<Result<Message>> Handle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        var countUsersInPost = await auth.Posts
            .Where(p => p.Id == request.Id)
            .Include(p => p.EmployeeUnits)
            .Include(p => p.Partners)
            .Select(p => new { PartnersCount = p.Partners.Count, EmployeeCount = p.EmployeeUnits.Count })
            .SingleOrDefaultAsync(cancellationToken);
        if (countUsersInPost is null ||
            countUsersInPost.EmployeeCount + countUsersInPost.PartnersCount > 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(PostErrors.PostUseOrEmpty);
        }

        await auth.Posts.Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Должность удалена"));
    }
}
