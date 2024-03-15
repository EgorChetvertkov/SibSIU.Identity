using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;

namespace SibSIU.Domain.UserManager.Users.Commands.RemovePupil;
public sealed class RemovePupilHandler(
    ILogger<RemovePupilHandler> logger,
    AuthContext auth) : IRemovePupilHandler
{
    public async Task<Result<Message>> Handle(RemovePupilRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(RemovePupilRequest request, CancellationToken cancellationToken)
    {
        await auth.Pupils
            .Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Школьник удален"));
    }
}
