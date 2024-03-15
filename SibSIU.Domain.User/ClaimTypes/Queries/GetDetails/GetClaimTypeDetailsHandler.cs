using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetDetails;
public sealed class GetClaimTypeDetailsHandler(
    ILogger<GetClaimTypeDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetClaimTypeDetailsHandler
{
    public async Task<Result<ClaimTypeDetails>> Handle(GetClaimTypeDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"claimType_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<ClaimTypeDetails>> InnerHandle(GetClaimTypeDetailsRequest request, CancellationToken cancellationToken)
    {
        ClaimTypeDetails? details = await auth.ClaimTypes
            .Where(ct => ct.Id == request.Id)
            .Select(ct => new ClaimTypeDetails()
            {
                Id = ct.Id,
                Name = ct.Name
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (details is null)
        {
            logger.LogInformation("School not found with id {Id}", request.Id);
            return CreateResult.Failure<ClaimTypeDetails>(ClaimTypeErrors.ClaimTypeNotFound);
        }

        return CreateResult.Success(details);
    }
}
