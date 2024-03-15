using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;
using SibSIU.CORS.Database.Entities;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Update;
public sealed class UpdateCORSHandler(
    ILogger<UpdateCORSHandler> logger,
    CORSContext cors) : IUpdateCORSHandler
{
    public async Task<Result<Message>> Handle(UpdateCORSRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await cors.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(UpdateCORSRequest request, CancellationToken cancellationToken)
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

        origin.CreateAt = DateTimeOffset.UtcNow;
        origin.Origin = request.Origin;

        await cors.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("CORS-политика изменена"));
    }
}
