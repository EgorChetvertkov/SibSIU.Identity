using System.Security.Claims;

namespace SibSIU.Identity.Infrastructure;

public static class UserExtensions
{
    public static Ulid GetUserId(this ClaimsPrincipal user, string userIdClaimType)
    {
        string userId = user.Claims
                .Where(u => u.Type == userIdClaimType)
                .Select(c => c.Value)
                .SingleOrDefault() ?? string.Empty;
        _ = Ulid.TryParse(userId, out Ulid ulidId);
        return ulidId;
    }

    public static string GetStringUserId(this ClaimsPrincipal user, string userIdClaimType)
    {
        string userId = user.Claims
                .Where(u => u.Type == userIdClaimType)
                .Select(c => c.Value)
                .SingleOrDefault() ?? string.Empty;
        return Ulid.TryParse(userId, out Ulid ulidId) ?
            userId :
            string.Empty;
    }

    public static bool IsInAnyRoles(this ClaimsPrincipal user, string[] roles)
    {
        foreach (var role in roles)
        {
            if (user.IsInRole(role))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsInAllRoles(this ClaimsPrincipal user, string[] roles)
    {
        bool isIn = true;

        foreach (var role in roles)
        {
            isIn = isIn && user.IsInRole(role);
        }

        return isIn;
    }
}
