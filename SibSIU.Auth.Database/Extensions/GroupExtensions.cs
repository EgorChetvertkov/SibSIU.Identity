using Microsoft.EntityFrameworkCore;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class GroupExtensions
{
    public static async Task<AcademicGroup?> GetByName(this DbSet<AcademicGroup> groups, string name, CancellationToken cancellationToken) =>
        await groups.Where(g => g.Name == name).SingleOrDefaultAsync(cancellationToken);
}
