using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

using OpenIddict.Abstractions;

using SibSIU.Core.Names;
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
            nameType: ClaimNames.Subject,
            roleType: ClaimNames.Role);

        identity.AddClaim(new(ClaimNames.Subject, user.UserName));

        if (user.EmailConfirmed)
        {
            identity.AddClaim(new(ClaimNames.EmailAddress, user.Email));
        }

        return identity;
    }

    public static ClaimsIdentity GetIdentity(UserDetails user)
    {
        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: ClaimNames.Subject,
            roleType: ClaimNames.Role);

        identity.AddClaim(ClaimNames.Subject, user.UserName);
        identity.AddClaim(ClaimNames.EmailAddress, user.Email);
        identity.AddClaim(ClaimNames.EmailVerified, user.EmailConfirmed.ToString());
        identity.AddClaim(ClaimNames.FirstName, user.FirstName);
        identity.AddClaim(ClaimNames.FamilyName, user.LastName);
        identity.AddClaim(ClaimNames.Patronymic, user.Patronymic);
        identity.AddClaim(ClaimNames.BirthDate, user.BirthOfDate.ToString());
        identity.AddClaim(ClaimNames.Gender, user.Gender.Name);
        identity.AddClaim(ClaimNames.PhoneNumber, user.PhoneNumber);
        identity.AddClaims(user.Works.Select(w => new Claim(ClaimNames.Worker, w.WorkPlaceId.ToString())));
        identity.AddClaims(user.Pupils.Select(p => new Claim(ClaimNames.Pupil, p.PupilId.ToString())));
        identity.AddClaims(user.Partners.Select(p => new Claim(ClaimNames.Partner, p.PartnerId.ToString())));
        identity.AddClaims(user.Students.Select(p => new Claim(ClaimNames.Student, p.StudentId.ToString())));
        identity.AddClaims(user.Claims.Select(c => new Claim(c.ClaimType.Name, c.Value)));
        identity.AddClaims(user.Roles.Select(r => new Claim(ClaimNames.Role, r.Name)));

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
            case ClaimNames.FirstName:
            case ClaimNames.FamilyName:
            case ClaimNames.Patronymic:
            case ClaimNames.BirthDate:
            case ClaimNames.Gender:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Profile) is true ||
                    claim.Subject?.HasScope(Scopes.OpenId) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            case ClaimNames.EmailAddress:
            case ClaimNames.EmailVerified:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Email) is true||
                    claim.Subject?.HasScope(Scopes.OpenId) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            case ClaimNames.PhoneNumber:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Phone) is true ||
                    claim.Subject?.HasScope(Scopes.OpenId) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            case ClaimNames.Role:
                yield return Destinations.AccessToken;

                if (claim.Subject?.HasScope(Scopes.Roles) is true ||
                    claim.Subject?.HasScope(Scopes.OpenId) is true)
                    yield return Destinations.IdentityToken;

                yield break;

            default:
                yield return Destinations.AccessToken;
                if (claim.Subject?.HasScope(Scopes.OpenId) is true)
                {
                    yield return Destinations.IdentityToken;
                }

                yield break;
        }
    }
}
