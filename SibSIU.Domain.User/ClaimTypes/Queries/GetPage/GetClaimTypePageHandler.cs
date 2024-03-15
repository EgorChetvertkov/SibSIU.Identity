using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.ClaimTypes;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetPage;
public sealed class GetClaimTypePageHandler(
    ILogger<GetClaimTypePageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetClaimTypePageHandler
{
    public async Task<Result<PaginationList<ClaimTypeRowItem>>> Handle(GetClaimTypePageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetClaimTypePageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<ClaimTypeRowItem>>> InnerHandle(GetClaimTypePageRequest request, CancellationToken cancellationToken)
    {
        var claimTypes = auth.ClaimTypes.SetFilter(request.Filter,
           claim =>
                claim.Name.ToLower().Contains(request.Filter.ToLower()));

        int countItems = await claimTypes.CountAsync(cancellationToken);

        claimTypes = claimTypes.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = claimTypes.Select(ct => new ClaimTypeRowItem(ct.Id, ct.Name));

        List<ClaimTypeRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<ClaimTypeRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<ClaimTypeRowItem>> GetPage(
        IQueryable<ClaimTypeRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<AuthClaimType, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(ClaimTypeRowItem.Id) => p => p.Id,
            nameof(ClaimTypeRowItem.Name) => p => p.Name,
            _ => p => p.Id,
        };
    }
}
