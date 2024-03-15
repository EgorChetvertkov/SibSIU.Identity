using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.UserData.Database.Entities;
using SibSIU.Core.Services.Extensions;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.Users.Queries.GetPage;
public sealed class GetUserPageHandler(
    ILogger<GetUserPageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUserPageHandler
{
    public async Task<Result<PaginationList<UserRowItem>>> Handle(GetUserPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetUserPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<UserRowItem>>> InnerHandler(GetUserPageRequest request, CancellationToken cancellationToken)
    {
        var users = auth.Users.AsNoTracking().AsQueryable().SetFilter(request.Filter, u =>
                        u.UserName.ToLower().Contains(request.Filter.ToLower()) ||
                        u.Email.ToLower().Contains(request.Filter.ToLower()) ||
                        u.FirstName.ToLower().Contains(request.Filter.ToLower()) ||
                        u.LastName.ToLower().Contains(request.Filter.ToLower()) ||
                        (u.Patronymic != null && u.Patronymic.ToLower().Contains(request.Filter.ToLower())));

        int countItems = await users.CountAsync(cancellationToken);

        users = users.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = users.Select(u => new UserRowItem(u.Id, u.UserName, u.FirstName, u.LastName, u.Patronymic));

        List<UserRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<UserRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<UserRowItem>> GetPage(
        IQueryable<UserRowItem> data,
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
            nameof(UserRowItem.Id) => u => u.Id,
            nameof(UserRowItem.FullName) => u => u.LastName,
            nameof(UserRowItem.UserName) => u => u.UserName,
            _ => u => u.Id,
        };
    }
}
