using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.CORS.Database;
using SibSIU.CORS.Database.Entities;
using SibSIU.Identity.Models.CORSes;

using System.Linq.Expressions;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetPage;
public sealed class GetCORSPageHandler(
    ILogger<GetCORSPageHandler> logger,
    IMemoryCache memory,
    CORSContext cors) : IGetCORSPageHandler
{
    public async Task<Result<PaginationList<CORSRowItem>>> Handle(GetCORSPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetCORSPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<CORSRowItem>>> InnerHandler(GetCORSPageRequest request, CancellationToken cancellationToken)
    {
        var policies = cors.Origins.AsNoTracking()
            .SetFilter(request.Filter, o =>
                o.Origin.ToLower().Contains(request.Filter.ToLower()));

        int countItems = await policies.CountAsync(cancellationToken);

        policies = policies.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = policies.Select(u => new CORSRowItem(u.Id, u.Origin));

        List<CORSRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<CORSRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<CORSRowItem>> GetPage(
        IQueryable<CORSRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<AllowOrigin, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(CORSRowItem.Id) => u => u.Id,
            nameof(CORSRowItem.Origin) => u => u.Origin,
            _ => u => u.Id,
        };
    }
}
