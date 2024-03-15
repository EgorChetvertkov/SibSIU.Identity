using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class ScopeExtensions
{
    public static async Task<Scope?> GetById(this IQueryable<Scope> scopes, Ulid id, CancellationToken cancellationToken) =>
        await scopes.Where(s => s.Id ==id).SingleOrDefaultAsync(cancellationToken);
}
