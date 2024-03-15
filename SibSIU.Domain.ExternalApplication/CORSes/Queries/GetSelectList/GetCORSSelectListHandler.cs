using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;
using SibSIU.CORS.Database.Entities;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetSelectList;
public sealed class GetCORSSelectListHandler(
    ILogger<GetCORSSelectListHandler> logger,
    IMemoryCache memory,
    CORSContext cors) : IGetCORSSelectListHandler
{
    public async Task<List<CORSItem>> Handle(GetCORSSelectListRequest request, CancellationToken cancellationToken)
    {
        int expire = string.IsNullOrWhiteSpace(request.Filter) ? 60 * 60 * 24 : 30;
        var result = await memory.WithMemoryCache($"CORS_policies_{request.Filter}", expire, request,
            async (request) =>  await InnerHandler(request, cancellationToken));
        return result.IsSuccess ? result.Data : [];
    }

    private async Task<Result<List<CORSItem>>> InnerHandler(GetCORSSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<AllowOrigin> policies = cors.Origins.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            policies = policies.Where(o => o.Origin.ToLower().Contains(request.Filter));
        }

        var list = await policies
            .Select(o => new CORSItem(o.Id, o.Origin))
            .ToListAsync(cancellationToken);

        return CreateResult.Success(list);
    }
}
