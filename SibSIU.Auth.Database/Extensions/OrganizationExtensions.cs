using Microsoft.EntityFrameworkCore;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class OrganizationExtensions
{
    public static async Task<Organization?> GetById(this DbSet<Organization> organizations, Ulid id, CancellationToken cancellationToken) =>
        await organizations.Where(o => o.Id == id).SingleOrDefaultAsync(cancellationToken);
}
