using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;
using SibSIU.CORS.Database.Entities;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Delete;
public sealed class DeleteCORSHandler(
    ILogger<DeleteCORSHandler> logger,
    CORSContext cors) : IDeleteCORSHandler
{
    public async Task<Result<Message>> Handle(DeleteCORSRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await cors.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(DeleteCORSRequest request, CancellationToken cancellationToken)
    {
        AllowOrigin? origin = await cors.Origins
            .Where(o => o.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (origin is null)
        {
            cors.Rollback();
            return CreateResult.Failure<Message>(CORSErrors.NotFound);
        }

        if (origin.Creator != request.Creator)
        {
            cors.Rollback();
            return CreateResult.Failure<Message>(CORSErrors.CreatorsNotEquals);
        }

        origin.UpdateAt = DateTimeOffset.UtcNow;
        origin.IsActive = false;
        await cors.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Политика удалена"));
    }
}
