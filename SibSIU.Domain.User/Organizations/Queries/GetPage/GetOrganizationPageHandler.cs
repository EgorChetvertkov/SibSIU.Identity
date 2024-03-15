using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Organizations;
using SibSIU.UserData.Database.Entities;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetPage;
public sealed class GetOrganizationPageHandler(
    ILogger<GetOrganizationPageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetOrganizationPageHandler
{
    public async Task<Result<PaginationList<OrganizationRowItem>>> Handle(GetOrganizationPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetOrganizationPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<OrganizationRowItem>>> InnerHandler(GetOrganizationPageRequest request, CancellationToken cancellationToken)
    {
        var organizations = auth.Organizations.AsNoTracking().SetFilter(request.Filter, o =>
                        o.FullName.ToLower().Contains(request.Filter.ToLower()) ||
                        o.ShortName.ToLower().Contains(request.Filter.ToLower()) ||
                        o.OGRN.ToLower().Contains(request.Filter.ToLower()) ||
                        o.TIN.ToLower().Contains(request.Filter.ToLower()) ||
                        o.KPP.ToLower().Contains(request.Filter.ToLower()));

        int countItems = await organizations.CountAsync(cancellationToken);

        organizations = organizations.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = organizations.Select(u => new OrganizationRowItem(u.Id, u.FullName, u.ShortName));

        List<OrganizationRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<OrganizationRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<OrganizationRowItem>> GetPage(
        IQueryable<OrganizationRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<Organization, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(OrganizationRowItem.Id) => u => u.Id,
            nameof(OrganizationRowItem.FullName) => u => u.FullName,
            nameof(OrganizationRowItem.ShortName) => u => u.ShortName,
            _ => u => u.Id,
        };
    }
}
