using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveWorkPlace;
public sealed class RemoveWorkPlaceHandler(
    ILogger<RemoveWorkPlaceHandler> logger,
    AuthContext auth) : IRemoveWorkPlaceHandler
{
    public async Task<Result<Message>> Handle(RemoveWorkPlaceRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(RemoveWorkPlaceRequest request, CancellationToken cancellationToken)
    {
        await auth.WorkPlaces
            .Where(s => s.Id == request.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.IsActive, false)
                .SetProperty(s => s.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Рабочее место откреплено от учетной записи"));
    }
}
