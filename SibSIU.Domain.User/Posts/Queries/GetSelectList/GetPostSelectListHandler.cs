using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Identity.Models.Posts;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetSelectList;
public sealed class GetPostSelectListHandler(
    ILogger<GetPostSelectListHandler> logger,
    AuthContext auth) : IGetPostSelectListHandler
{
    public async Task<List<PostItem>> Handle(GetPostSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Post> posts = auth.Posts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            posts = posts.Where(o => o.Name.ToLower().Contains(request.Filter));
        }

        return await posts
            .Select(p => new PostItem(p.Id, p.Name))
            .ToListAsync(cancellationToken);
    }
}
