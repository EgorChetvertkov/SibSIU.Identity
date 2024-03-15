using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetDetails;
public sealed class GetScopeDetailsHandler(
    ILogger<GetScopeDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetScopeDetailsHandler
{
    public async Task<Result<ScopeDetails>> Handle(GetScopeDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"scope_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<ScopeDetails>> InnerHandle(GetScopeDetailsRequest request, CancellationToken cancellationToken)
    {
        ScopeDetails? details = await auth.Scopes
            .Where(s => s.Id == request.Id)
            .Select(s => new ScopeDetails()
            {
                Id = s.Id,
                Name = s.Name
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (details is null)
        {
            logger.LogInformation("School not found with id {Id}", request.Id);
            return CreateResult.Failure<ScopeDetails>(ScopeErrors.ScopeNotFound);
        }

        return CreateResult.Success(details);
    }
}
