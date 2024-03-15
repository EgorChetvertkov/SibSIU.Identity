using Microsoft.EntityFrameworkCore;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class UnitExtensions
{
    public static async Task<Unit?> GetById(this DbSet<Unit> units, Ulid id, CancellationToken cancellationToken) =>
        await units.Where(u => u.Id == id).SingleOrDefaultAsync(cancellationToken);
}
