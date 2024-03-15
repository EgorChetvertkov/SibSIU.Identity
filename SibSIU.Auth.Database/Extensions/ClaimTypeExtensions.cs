using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class ClaimTypeExtensions
{
    public static async Task<AuthClaimType?> GetById(this IQueryable<AuthClaimType> claimTypes, Ulid id, CancellationToken cancellationToken) =>
        await claimTypes.Where(ct => ct.Id == id).SingleOrDefaultAsync(cancellationToken);
}
