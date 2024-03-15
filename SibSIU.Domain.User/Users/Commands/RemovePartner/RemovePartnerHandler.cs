using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;

namespace SibSIU.Domain.UserManager.Users.Commands.RemovePartner;
public sealed class RemovePartnerHandler(
    ILogger<RemovePartnerHandler> logger,
    AuthContext auth) : IRemovePartnerHandler
{
    public async Task<Result<Message>> Handle(RemovePartnerRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
            await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(RemovePartnerRequest request, CancellationToken cancellationToken)
    {
        await auth.Partners
            .Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Партнер удален"));
    }
}
