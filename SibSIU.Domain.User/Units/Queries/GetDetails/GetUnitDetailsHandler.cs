using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Domain.UserManager.Units.Queries.GetDetails;
public sealed class GetUnitDetailsHandler(
    ILogger<GetUnitDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUnitDetailsHandler
{
    public async Task<Result<UnitDetails>> Handle(GetUnitDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"school_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<UnitDetails>> InnerHandle(GetUnitDetailsRequest request, CancellationToken cancellationToken)
    {
        UnitDetails? details = await auth.Units
            .Where(p => p.Id == request.Id)
            .Select(p => new UnitDetails()
            {
                Id = p.Id,
                FullName = p.FullName,
                ShortName = p.ShortName,
                Parent = p.Parent != null ? new(p.Parent.Id, p.Parent.FullName, p.Parent.ShortName) : new(),
                Children = p.Children.Select(c => new UnitItem(c.Id, c.FullName, c.ShortName)).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (details is null)
        {
            logger.LogInformation("Unit not found with id {Id}", request.Id);
            return CreateResult.Failure<UnitDetails>(UnitErrors.UnitNotFound);
        }

        return CreateResult.Success(details);
    }
}
