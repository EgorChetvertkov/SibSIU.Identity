using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.DeleteByCreator;
public sealed class DeleteCORSByCreatorHandler(
    ILogger<DeleteCORSByCreatorHandler> logger,
    CORSContext cors) : IDeleteCORSByCreatorHandler
{
    public async Task<Result<Message>> Handle(DeleteCORSByCreatorRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await cors.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(DeleteCORSByCreatorRequest request, CancellationToken cancellationToken)
    {
        await cors.Origins
            .Where(o => o.Creator == request.Creator)
            .ExecuteUpdateAsync(o => o
                .SetProperty(o => o.IsActive, false)
                .SetProperty(o => o.UpdateAt, DateTimeOffset.UtcNow),
                cancellationToken);
        await cors.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message($"Все политики полученные от '{request.Creator}' удалены"));
    }
}
