using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;
using SibSIU.CORS.Database.Entities;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Create;
public sealed class CreateCORSHandler(
    ILogger<CreateCORSHandler> logger,
    CORSContext cors) : ICreateCORSHandler
{
    public async Task<Result<Message>> Handle(CreateCORSRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await cors.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(CreateCORSRequest request, CancellationToken cancellationToken)
    {
        List<AllowOrigin> origins = [];

        DateTimeOffset now;
        foreach (var origin in request.Origins)
        {
            now = DateTimeOffset.UtcNow;
            origins.Add(new()
            {
                Id = Ulid.NewUlid(now),
                CreateAt = now,
                UpdateAt = now,
                IsActive = true,
                Creator = request.Creator,
                Origin = origin
            });
        }

        if (origins.Count > 0)
        {
            await cors.AddRangeAsync(origins, cancellationToken);
            await cors.SaveChangesAsync(cancellationToken);
        }

        return CreateResult.Success(new Message("Адреса успешно установлены в разрешенные"));
    }
}
