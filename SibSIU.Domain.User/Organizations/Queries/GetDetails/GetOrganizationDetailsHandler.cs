using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Organizations;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetDetails;
public sealed class GetOrganizationDetailsHandler(
    ILogger<GetOrganizationDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth)
    : IGetOrganizationDetailsHandler
{
    public async Task<Result<OrganizationDetails>> Handle(GetOrganizationDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"organization_{request.Id}", 30, request,
            async (request) => await InnerHandler(request, cancellationToken));
    }

    public async Task<Result<OrganizationDetails>> InnerHandler(GetOrganizationDetailsRequest request, CancellationToken cancellationToken)
    {
        OrganizationDetails? organization = await auth.Organizations
            .Where(o => o.Id == request.Id)
            .Select(o => new OrganizationDetails()
            {
                Id = o.Id,
                FullName = o.FullName,
                ShortName = o.ShortName,
                OGRN = o.OGRN,
                TIN = o.TIN,
                KPP = o.KPP
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (organization is null)
        {
            logger.LogInformation("Organization not found with id {Id}", request.Id);
            return CreateResult.Failure<OrganizationDetails>(OrganizationErrors.OrganizationNotFound);
        }

        return CreateResult.Success(organization);
    }
}
