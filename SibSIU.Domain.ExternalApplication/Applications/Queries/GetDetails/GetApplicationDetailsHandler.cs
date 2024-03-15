using OpenIddict.Abstractions;

using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Errors;
using SibSIU.Identity.Models.Applications;

namespace SibSIU.Domain.ExternalApplication.Applications.Queries.GetDetails;
public sealed class GetApplicationDetailsHandler(
    IOpenIddictApplicationManager applicationManager) : IGetApplicationDetailsHandler
{
    public async Task<Result<ApplicationDetails>> Handle(GetApplicationDetailsRequest request, CancellationToken cancellationToken)
    {
        var app = await applicationManager.FindByClientIdAsync(request.Id.Value, CancellationToken.None);
        if (app is null)
        {
            return CreateResult.Failure<ApplicationDetails>(ApplicationErrors.NotFound);
        }

        var descriptorFromExisting = new OpenIddictApplicationDescriptor();
        await applicationManager.PopulateAsync(descriptorFromExisting, app, cancellationToken);

        ApplicationDetails details = new()
        {
            ApplicationType = descriptorFromExisting.ApplicationType ?? string.Empty,
            Settings = descriptorFromExisting.Settings,
            ClientSecret = descriptorFromExisting.ClientSecret,
            DisplayName = descriptorFromExisting.DisplayName ?? string.Empty,
            ClientId = descriptorFromExisting.ClientId ?? string.Empty,
            ClientType = descriptorFromExisting.ClientType ?? string.Empty,
            ConsentType = descriptorFromExisting.ConsentType ?? string.Empty,
            JSONWebKeySet = string.Empty,
            Permissions = [.. descriptorFromExisting.Permissions],
            PostLogoutRedirectUris = [.. descriptorFromExisting.PostLogoutRedirectUris],
            RedirectUris = [.. descriptorFromExisting.RedirectUris],
            Requirements = [.. descriptorFromExisting.Requirements],
        };

        return CreateResult.Success(details);
    }
}
