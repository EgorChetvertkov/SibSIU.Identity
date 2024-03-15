using Microsoft.IdentityModel.Tokens;

using OpenIddict.Abstractions;

using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Errors;

using System.Security.Cryptography;

using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands._Shared;
public sealed class CreateOrUpdateApplicationRequest : IRequest<Result<Message>>, IValidated
{
    public string ApplicationType { get; }
    public string ClientId { get; }
    public string ClientType { get; }
    public bool IsConfidentialClient { get; } 
    public string ClientSecret { get; set; }
    public string JSONWebKeySet { get; set; }
    public string ConsentType { get; }
    public string DisplayName { get; }
    public List<string> Permissions { get; }
    public List<Uri> RedirectUris { get; }
    public List<Uri> PostLogoutRedirectUris { get; }
    public List<string> Requirements { get; }
    public Dictionary<string, string> Settings { get; }

    public CreateOrUpdateApplicationRequest(
        string applicationType,
        string clientId,
        string clientType,
        string clientSecret,
        string jSONWebKeySet,
        string consentType,
        string displayName,
        List<string> permissions,
        List<Uri> redirectUris,
        List<Uri> postLogoutRedirectUris,
        List<string> requirements,
        Dictionary<string, string> settings)
    {
        ApplicationType = applicationType.TrimOrEmpty();
        ClientId = clientId.TrimOrEmpty();
        ClientType = clientType.TrimOrEmpty();
        IsConfidentialClient = ClientType?.Equals(ClientTypes.Confidential) ?? false;
        ClientSecret = clientSecret.TrimOrEmpty();
        JSONWebKeySet = jSONWebKeySet.TrimOrEmpty();
        ConsentType = consentType.TrimOrEmpty();
        DisplayName = displayName.TrimOrEmpty();
        Permissions = permissions ?? [];
        RedirectUris = redirectUris ?? [];
        PostLogoutRedirectUris = postLogoutRedirectUris ?? [];
        Requirements = requirements ?? [];
        Settings = settings ?? [];
    }

    public CreateOrUpdateApplicationRequest()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              [], [], [], [], []) { }

    public Error Validate()
    {
        if (ApplicationType is not ApplicationTypes.Native and not ApplicationTypes.Web)
        {
            return ApplicationErrors.InvalidApplicationType;
        }

        if (string.IsNullOrWhiteSpace(ClientId))
        {
            return ApplicationErrors.InvalidClientId;
        }

        if (ClientType is not ClientTypes.Confidential and not ClientTypes.Public)
        {
            return ApplicationErrors.InvalidClientType;
        }

        if (string.IsNullOrWhiteSpace(ClientSecret))
        {
            return ApplicationErrors.InvalidClientSecret;
        }

        if (string.IsNullOrWhiteSpace(DisplayName))
        {
            return ApplicationErrors.InvalidDisplayName;
        }

        return Error.None;
    }

    public OpenIddictApplicationDescriptor GetDescriptor()
    {
        JsonWebKeySet? jsonWebKeySet = string.IsNullOrWhiteSpace(JSONWebKeySet) ?
            default :
            new JsonWebKeySet() { Keys = { JsonWebKeyConverter.ConvertFromECDsaSecurityKey(GetECDsaSigningKey(JSONWebKeySet)) } };

        OpenIddictApplicationDescriptor openIdApplicationDescriptor = new()
        {
            ApplicationType = ApplicationType,
            ClientId = ClientId,
            ClientType = ClientType,
            ClientSecret = ClientSecret,
            JsonWebKeySet = jsonWebKeySet,
            ConsentType = ConsentType,
            DisplayName = DisplayName,
        };

        foreach (var permissions in Permissions)
        {
            openIdApplicationDescriptor.Permissions.Add(permissions);
        }

        foreach (var redirectUri in RedirectUris)
        {
            openIdApplicationDescriptor.RedirectUris.Add(redirectUri);
        }

        foreach (var postLogoutRedirectUri in PostLogoutRedirectUris)
        {
            openIdApplicationDescriptor.PostLogoutRedirectUris.Add(postLogoutRedirectUri);
        }

        foreach (var requirement in Requirements)
        {
            openIdApplicationDescriptor.Requirements.Add(requirement);
        }

        foreach (var setting in Settings)
        {
            openIdApplicationDescriptor.Settings.Add(setting.Key, setting.Value);
        }

        return openIdApplicationDescriptor;
    }

    private static ECDsaSecurityKey GetECDsaSigningKey(ReadOnlySpan<char> key)
    {
        var algorithm = ECDsa.Create();
        algorithm.ImportFromPem(key);
        return new ECDsaSecurityKey(algorithm);
    }
}
