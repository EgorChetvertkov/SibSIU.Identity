using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.UserData.Database.Entities;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedPage;
public sealed class GetUnconfirmedUserPageHandler(
    ILogger<GetUnconfirmedUserPageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUnconfirmedUserPageHandler
{
    public async Task<Result<PaginationList<UnconfirmedUserRowItem>>> Handle(GetUnconfirmedUserPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetUnconfirmedUserPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<UnconfirmedUserRowItem>>> InnerHandler(GetUnconfirmedUserPageRequest request, CancellationToken cancellationToken)
    {
        var users = auth.Users
            .IgnoreQueryFilters()
            .AsQueryable()
            .SetFilter(request.Filter, u => u.IsActive && !u.IsConfirmedUser &&
                        (u.UserName.ToLower().Contains(request.Filter.ToLower()) ||
                        u.Email.ToLower().Contains(request.Filter.ToLower()) ||
                        u.FirstName.ToLower().Contains(request.Filter.ToLower()) ||
                        u.LastName.ToLower().Contains(request.Filter.ToLower()) ||
                        (u.Patronymic != null && u.Patronymic.ToLower().Contains(request.Filter.ToLower()))));

        int countItems = await users.CountAsync(cancellationToken);

        users = users.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = users.Select(u => new UnconfirmedUserRowItem(
            u.Id, u.UserName, u.FirstName, u.LastName, u.Patronymic, u.Pupils.Count, u.Students.Count, u.Partners.Count));

        List<UnconfirmedUserRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<UnconfirmedUserRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<UnconfirmedUserRowItem>> GetPage(
        IQueryable<UnconfirmedUserRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<User, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(UnconfirmedUserRowItem.Id) => u => u.Id,
            nameof(UnconfirmedUserRowItem.FullName) => u => u.LastName,
            nameof(UnconfirmedUserRowItem.UserName) => u => u.UserName,
            _ => u => u.Id,
        };
    }
}
