using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveClaim;
public sealed class RemoveClaimHandler(
    ILogger<RemoveClaimHandler> logger,
    AuthContext auth) : IRemoveClaimHandler
{
    public async Task<Result<Message>> Handle(RemoveClaimRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
            await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(RemoveClaimRequest request, CancellationToken cancellationToken)
    {
        await auth.Claims
            .Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Утверждение удалено"));
    }
}
