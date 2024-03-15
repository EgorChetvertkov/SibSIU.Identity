using Microsoft.Extensions.Caching.Memory;

using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Applications;

namespace SibSIU.Domain.ExternalApplication.Applications.Queries.GetPage;
public sealed class GetApplicationPageHandler(
    IMemoryCache memory,
    IOpenIddictApplicationManager applicationManager) : IGetApplicationPageHandler
{
    public async Task<Result<PaginationList<ApplicationRowItem>>> Handle(GetApplicationPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetApplicationPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<ApplicationRowItem>>> InnerHandle(GetApplicationPageRequest request, CancellationToken cancellationToken)
    {
        Func<IQueryable<object>, IQueryable<OpenIddictEntityFrameworkCoreApplication>> query = string.IsNullOrEmpty(request.Filter)
            ? ((apps) => apps.Where(app => true).Skip(request.GetSkipCount()).Take(request.GetTakeCount())
            .Select(s => s as OpenIddictEntityFrameworkCoreApplication).OrderBy(s => s.ClientId!)
            .AsQueryable())
            : ((apps) => apps.Where(app => (app as OpenIddictEntityFrameworkCoreApplication).DisplayName.Contains(request.Filter)
            || (app as OpenIddictEntityFrameworkCoreApplication).ClientId.Contains(request.Filter))
               .Skip(request.GetSkipCount()).Take(request.GetTakeCount()).Select(s => s as OpenIddictEntityFrameworkCoreApplication).OrderBy(s => s.ClientId!)
               .AsQueryable());
        
        long countItems = string.IsNullOrEmpty(request.Filter) ?
            await applicationManager.CountAsync(cancellationToken) :
            await applicationManager.CountAsync<object>(query, cancellationToken);
        List<ApplicationRowItem> page = [];

        await foreach (var app in applicationManager.ListAsync(query, cancellationToken))
        {
            page.Add(new(
                app.ClientId ?? string.Empty,
                app.DisplayName ?? string.Empty));
        }

        var resultPage = new PaginationList<ApplicationRowItem>(
                page,
                (int)countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }
}
