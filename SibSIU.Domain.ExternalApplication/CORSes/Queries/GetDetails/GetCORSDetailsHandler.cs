using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;
using SibSIU.Domain.ExternalApplication.Errors;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetDetails;
public sealed class GetCORSDetailsHandler(
    ILogger<GetCORSDetailsHandler> logger,
    IMemoryCache memory,
    CORSContext cors) : IGetCORSDetailsHandler
{
    public async Task<Result<CORSDetails>> Handle(GetCORSDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"cors_{request.Id}", 30, request,
            async (request) => await InnerHandler(request, cancellationToken));
    }

    private async Task<Result<CORSDetails>> InnerHandler(GetCORSDetailsRequest request, CancellationToken cancellationToken)
    {
        CORSDetails? policy = await cors.Origins
            .Where(o => o.Id == request.Id)
            .Select(o => new CORSDetails()
            {
                Id = o.Id,
                Origin = o.Origin
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (policy is null)
        {
            logger.LogInformation("CORS-policy not found with id {Id}", request.Id);
            return CreateResult.Failure<CORSDetails>(CORSErrors.NotFound);
        }

        return CreateResult.Success(policy);
    }
}
