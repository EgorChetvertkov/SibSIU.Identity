using Microsoft.EntityFrameworkCore;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Extensions;
public static class UserExtensions
{
    public static string GetFullName(string firstName, string lastName, string? patronymic = null) =>
        $"{lastName} {firstName} {patronymic ?? string.Empty}".Trim();
    public static string FullName(this User user) => GetFullName(user.FirstName, user.LastName, user.Patronymic);

    public static async Task<User?> GetById(this DbSet<User> users, Ulid id, CancellationToken cancellationToken) =>
        await users.Where(u => u.Id == id).SingleOrDefaultAsync(cancellationToken);

    public static async Task<User?> GetUserByIdWithStudents(this DbSet<User> users, Ulid id, CancellationToken cancellationToken) =>
        await users.Where(u => u.Id == id)
        .Include(u => u.Students)
            .ThenInclude(s => s.Group)
        .SingleOrDefaultAsync(cancellationToken);

    public static async Task<T?> GetUserByIdMapTo<T>(this IQueryable<User> users, Ulid id, Func<User, T> map, CancellationToken cancellationToken) =>
        await users
            .Where(u => u.Id == id)
            .Include(u => u.Students)
                .ThenInclude(s => s.Group)
            .Include(u => u.WorkPlaces)
                .ThenInclude(wp => wp.Unit)
            .Include(u => u.WorkPlaces)
                .ThenInclude(wp => wp.Post)
            .Include(u => u.Gender)
            .Include(u => u.Partners)
                .ThenInclude(p => p!.Organization)
            .Include(u => u.Partners)
                .ThenInclude(p => p!.Post)
            .Include(u => u.Pupils)
                .ThenInclude(p => p!.School)
            .Include(u => u.Claims)
                .ThenInclude(c => c.ClaimType)
                    .ThenInclude(ct => ct.Settings)
                        .ThenInclude(s => s.Scope)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Select(u => map(u))
            .SingleOrDefaultAsync(cancellationToken);

    public static async Task<bool> AnyUserHasUserName(this DbSet<User> users, string userName, CancellationToken cancellationToken) =>
        await users.Where(u => u.UserName == userName).AnyAsync(cancellationToken);

    public static async Task<bool> AnyUserHasEmail(this DbSet<User> users, string email, CancellationToken cancellationToken) =>
        await users.Where(u => u.Email == email).AnyAsync(cancellationToken);

    public static async Task<bool> AnyUserHasPhone(this DbSet<User> users, string phone, CancellationToken cancellationToken) =>
        await users.Where(u => u.PhoneNumber == phone).AnyAsync(cancellationToken);
}
