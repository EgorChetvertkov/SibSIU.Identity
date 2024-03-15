using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Schools;
using SibSIU.UserData.Database.Entities;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetPage;
public sealed class GetSchoolPageHandler(
    ILogger<GetSchoolPageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetSchoolPageHandler
{
    public async Task<Result<PaginationList<SchoolRowItem>>> Handle(GetSchoolPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetSchoolPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<SchoolRowItem>>> InnerHandle(GetSchoolPageRequest request, CancellationToken cancellationToken)
    {
        var schools = auth.Schools.AsNoTracking().SetFilter(request.Filter, s =>
                s.FullName.ToLower().Contains(request.Filter.ToLower()) ||
                s.ShortName.ToLower().Contains(request.Filter.ToLower()));

        int countItems = await schools.CountAsync(cancellationToken);

        schools = schools.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = schools.Select(s => new SchoolRowItem(s.Id, s.FullName, s.ShortName));

        List<SchoolRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<SchoolRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<SchoolRowItem>> GetPage(
        IQueryable<SchoolRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<School, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(SchoolRowItem.Id) => p => p.Id,
            nameof(SchoolRowItem.FullName) => p => p.FullName,
            nameof(SchoolRowItem.ShortName) => p => p.ShortName,
            _ => p => p.Id,
        };
    }
}
