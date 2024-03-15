using Microsoft.EntityFrameworkCore;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class SchoolExtensions
{
    public static async Task<School?> GetById(this DbSet<School> schools, Ulid id, CancellationToken cancellationToken) =>
        await schools.Where(s => s.Id == id).SingleOrDefaultAsync(cancellationToken);
}
