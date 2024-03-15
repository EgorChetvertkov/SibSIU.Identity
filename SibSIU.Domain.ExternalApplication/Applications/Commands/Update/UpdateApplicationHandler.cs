using OpenIddict.Abstractions;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Applications.Commands._Shared;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.Create;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.DeleteByCreator;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands.Update;
public sealed class UpdateApplicationHandler(
    IOpenIddictApplicationManager applicationManager,
    ICreateCORSHandler createCORS,
    IDeleteCORSByCreatorHandler deleteCORS) : IUpdateApplicationHandler
{
    public Task<Result<Message>> Handle(CreateOrUpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        return request.Ensure(async (request) =>
            await InnerHandler(request, cancellationToken));
    }

    private async Task<Result<Message>> InnerHandler(CreateOrUpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        var existing = await applicationManager.FindByClientIdAsync(request.ClientId, cancellationToken);
        if (existing is null)
        {
            return CreateResult.Failure<Message>(ApplicationErrors.NotFound);
        }

        var openIdApplicationDescriptor = request.GetDescriptor();
        var descriptorFromExisting = new OpenIddictApplicationDescriptor();
        await applicationManager.PopulateAsync(descriptorFromExisting, existing);

        if (request.IsConfidentialClient)
        {
            if (string.IsNullOrEmpty(request.ClientSecret))
            {
                openIdApplicationDescriptor.ClientSecret = descriptorFromExisting.ClientSecret;
            }
            if (string.IsNullOrEmpty(request.JSONWebKeySet))
            {
                openIdApplicationDescriptor.JsonWebKeySet = descriptorFromExisting.JsonWebKeySet;
            }
        }

        if (!openIdApplicationDescriptor.RedirectUris.SequenceEqual(descriptorFromExisting.RedirectUris))
        {
            await deleteCORS.Handle(new(descriptorFromExisting.ClientId ?? string.Empty),
                cancellationToken);
            await createCORS.Handle(new (openIdApplicationDescriptor.ClientId ?? string.Empty,
                openIdApplicationDescriptor.RedirectUris.Select(u => u
                .GetLeftPart(UriPartial.Authority)).ToList()),
                cancellationToken);
        }

        await applicationManager.UpdateAsync(existing, openIdApplicationDescriptor, cancellationToken);

        return CreateResult.Success(new Message("Приложение обновлено"));
    }
}
