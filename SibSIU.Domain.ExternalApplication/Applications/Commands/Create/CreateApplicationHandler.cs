using OpenIddict.Abstractions;

using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.ExternalApplication.Applications.Commands._Shared;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.Create;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands.Create;
public sealed class CreateApplicationHandler(
    IOpenIddictApplicationManager applicationManager,
    ICreateCORSHandler createCORS) : ICreateApplicationHandler
{
    public Task<Result<Message>> Handle(CreateOrUpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        return request.Ensure(async (request) =>
            await InnerHandler(request, cancellationToken));
    }

    private async Task<Result<Message>> InnerHandler(CreateOrUpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        await applicationManager.CreateAsync(request.GetDescriptor(), cancellationToken);
        List<string> URLs = request.RedirectUris.Select(u => u.GetLeftPart(UriPartial.Authority)).ToList();

        if (URLs.Count != 0)
        {
            await createCORS.Handle(new(request.ClientId, URLs), cancellationToken);
        }

        return CreateResult.Success(new Message("Приложение успешно зарегистрировано"));
    }
}
