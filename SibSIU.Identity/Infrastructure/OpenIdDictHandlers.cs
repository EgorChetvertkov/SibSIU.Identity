using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

using OpenIddict.Abstractions;

using SibSIU.Identity.Models.User.Manage;

using System.Security.Claims;

using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SibSIU.Identity.Infrastructure;

public class OpenIdDictHandlers
{
    public static ClaimsIdentity GetAuthCodeIdentity(UserDetails user)
    {
        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Subject,
            roleType: Claims.Role);

        identity.AddClaim(new(Claims.Subject, user.UserName));

        if (user.EmailConfirmed)
        {
            identity.AddClaim(new(Claims.Email, user.Email));
        }

        identity.SetDestinations(GetDestinations);

        return identity;
    }

    public static ClaimsIdentity GetIdentity(UserDetails user)
    {
        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Subject,
            roleType: Claims.Role);

        identity.AddClaim(Claims.Subject, user.UserName);
        identity.AddClaim(Claims.Email, user.Email);
        identity.AddClaim(Claims.EmailVerified, user.EmailConfirmed.ToString());
        identity.AddClaim(Claims.Name, user.FirstName);
        identity.AddClaim(Claims.MiddleName, user.LastName);
        identity.AddClaim(Claims.FamilyName, user.Patronymic);
        identity.AddClaim(Claims.Birthdate, user.BirthOfDate.ToString());
        identity.AddClaim(Claims.Gender, user.Gender.Name);
        identity.AddClaim(Claims.PhoneNumber, user.PhoneNumber);
        identity.AddClaims(user.Works.Select(w => new Claim("workers", w.WorkPlaceId.ToString())));
        identity.AddClaims(user.Pupils.Select(p => new Claim("pupils", p.PupilId.ToString())));
        identity.AddClaims(user.Partners.Select(p => new Claim("partners", p.PartnerId.ToString())));
        identity.AddClaims(user.Students.Select(p => new Claim("students", p.StudentId.ToString())));

        foreach (var item in user.Claims)
        {
            identity.AddClaim(new Claim(item.ClaimType.Name, item.Value));
        }

        identity.SetDestinations(GetDestinations);

        return identity;
    }

    public static Dictionary<string, StringValues> ParseOAuthParameters(HttpContext httpContext, List<string>? excluding = null)
    {
        excluding ??= [];

        var parameters = httpContext.Request.HasFormContentType
            ? httpContext.Request.Form
                .Where(v => !excluding.Contains(v.Key))
                .ToDictionary(v => v.Key, v => v.Value)
            : httpContext.Request.Query
                .Where(v => !excluding.Contains(v.Key))
                .ToDictionary(v => v.Key, v => v.Value);

        return parameters;
    }

    public static string BuildRedirectUrl(HttpRequest request, IDictionary<string, StringValues> oAuthParameters)
    {
        return request.PathBase + request.Path + QueryString.Create(oAuthParameters);
    }

    public static bool IsAuthenticated(AuthenticateResult authenticateResult, OpenIddictRequest request)
    {
        if (!authenticateResult.Succeeded)
        {
            return false;
        }

        if (request.MaxAge.HasValue && authenticateResult.Properties != null)
        {
            var maxAgeSeconds = TimeSpan.FromSeconds(request.MaxAge.Value);

            var expired = !authenticateResult.Properties.IssuedUtc.HasValue ||
                          DateTimeOffset.UtcNow - authenticateResult.Properties.IssuedUtc > maxAgeSeconds;

            return !expired;
        }

        return true;
    }

    public static IEnumerable<string> GetDestinations(Claim claim)
    {
        switch (claim.Type)
        {
            case Claims.Name:
            case Claims.FamilyName:
            case Claims.MiddleName:
            case Claims.Birthdate:
            case Claims.Gender:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Profile) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Email:
            case Claims.EmailVerified:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Email) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.PhoneNumber:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Phone) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Role:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Roles) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            default:
                if (claim.Subject?.HasScope(Scopes.OpenId) is true)
                {
                    yield return Destinations.IdentityToken;
                }

                yield break;
        }
    }
}
