using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Posts;
using SibSIU.UserData.Database.Entities;

using System.Linq.Expressions;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetPage;
public sealed class GetPostPageHandler(
    ILogger<GetPostPageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetPostPageHandler
{
    public async Task<Result<PaginationList<PostRowItem>>> Handle(GetPostPageRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"{nameof(GetPostPageRequest)}-{request.Filter}-{request.PageNumber}-{request.PageSize}-{request.SortField}-{request.SortType}",
                30, request, async (request) => await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<PaginationList<PostRowItem>>> InnerHandle(GetPostPageRequest request, CancellationToken cancellationToken)
    {
        var posts = auth.Posts.AsNoTracking().SetFilter(
            request.Filter,
            p => p.Name.ToLower().Contains(request.Filter.ToLower()));

        int countItems = await posts.CountAsync(cancellationToken);

        posts = posts.SetOrder(request.SortType, GetSelector(request.SortField));

        var data = posts.Select(p => new PostRowItem(p.Id, p.Name));

        List<PostRowItem> page = await GetPage(data, request.GetSkipCount(), request.GetTakeCount(), cancellationToken);

        var resultPage = new PaginationList<PostRowItem>(
                page,
                countItems,
                request.PageNumber,
                request.PageSize);

        return CreateResult.Success(resultPage);
    }

    private static async Task<List<PostRowItem>> GetPage(
        IQueryable<PostRowItem> data,
        int countSkip,
        int countTake,
        CancellationToken cancellationToken)
    {
        return await data
                    .Skip(countSkip)
                    .Take(countTake)
                    .ToListAsync(cancellationToken);
    }

    private static Expression<Func<Post, object>> GetSelector(string sortingField)
    {
        return sortingField switch
        {
            nameof(PostRowItem.Id) => p => p.Id,
            nameof(PostRowItem.Name) => p => p.Name,
            _ => p => p.Id,
        };
    }
}
