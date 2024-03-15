using OpenIddict.Abstractions;

using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.DeleteByCreator;
using SibSIU.Domain.ExternalApplication.Errors;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands.Delete;
public sealed class DeleteApplicationHandler(
    IOpenIddictApplicationManager applicationManager,
    IDeleteCORSByCreatorHandler deleteCORS) : IDeleteApplicationHandler
{
    public async Task<Result<Message>> Handle(DeleteApplicationRequest request, CancellationToken cancellationToken)
    {
        var existing = await applicationManager.FindByClientIdAsync(request.Id.Value);
        if (existing is null)
        {
            return CreateResult.Failure<Message>(ApplicationErrors.NotFound);
        }

        var descriptorFromExisting = new OpenIddictApplicationDescriptor();
        await applicationManager.PopulateAsync(descriptorFromExisting, existing, cancellationToken);

        await applicationManager.DeleteAsync(existing, cancellationToken);
        if (descriptorFromExisting.RedirectUris.Count != 0)
        {
            await deleteCORS.Handle(new(descriptorFromExisting.ClientId ?? string.Empty), cancellationToken);
        }

        return CreateResult.Success(new Message("Приложение удалено"));
    }
}
