using Microsoft.EntityFrameworkCore;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class PostsExtensions
{
    public static async Task<Post?> GetById(this DbSet<Post> posts, Ulid id, CancellationToken cancellationToken) =>
        await posts.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
}
