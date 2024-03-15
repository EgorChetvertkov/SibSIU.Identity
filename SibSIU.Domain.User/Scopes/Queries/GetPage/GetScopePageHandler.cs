using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Scopes;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetPage;
public sealed class GetScopePageHandler(
    ILogger<GetScopePageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetScopePageHandler
{
    public async Task<Result<PaginationList<ScopeRowItem>>> Handle(GetScopePageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetScopePageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<ScopeRowItem>>> InnerHandle(GetScopePageRequest request, CancellationToken cancellationToken)
    {
        var scopes = auth.Scopes.SetFilter(request.Filter,
            s =>
                s.Name.ToLower().Contains(request.Filter.ToLower()));

        int countItems = await scopes.CountAsync(cancellationToken);

        scopes = scopes.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = scopes.Select(ct => new ScopeRowItem(ct.Id, ct.Name));

        List<ScopeRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<ScopeRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<ScopeRowItem>> GetPage(
        IQueryable<ScopeRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<Scope, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(ScopeRowItem.Id) => p => p.Id,
            nameof(ScopeRowItem.Name) => p => p.Name,
            _ => p => p.Id,
        };
    }
}
