using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Units;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Units.Queries.GetPage;
public sealed class GetUnitPageHandler(
    ILogger<GetUnitPageHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUnitPageHandler
{
    public async Task<Result<UnitRowItem>> Handle(GetUnitPageRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"{nameof(GetUnitPageRequest)}",
                30, request, async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<UnitRowItem>> InnerHandle(GetUnitPageRequest request, CancellationToken cancellationToken)
    {
        var units = await auth.Units
            .ToListAsync(cancellationToken);

        Unit? seed = units.Where(u => u.ParentId == null).SingleOrDefault();
        if (seed is null)
        {
            return CreateResult.Failure<UnitRowItem>(UnitErrors.NotFoundSeedUnit);
        }

        var result = UnitWithChildren(seed, units);
        if (result is null)
        {
            return CreateResult.Failure<UnitRowItem>(UnitErrors.NotFoundSeedUnit);
        }

        return CreateResult.Success(result);
    }

    private UnitRowItem? UnitWithChildren(Unit seed, List<Unit> units)
    {
        var unitChildren = units.Where(u => u.Id == seed.Id).ToList();
        if (unitChildren.Count == 0)
        {
            return null;
        }

        List<UnitRowItem> unitRows = [];
        foreach (var item in unitChildren)
        {
            var next = UnitWithChildren(item, unitChildren);
            if (next is not null)
            {
                unitRows.Add(next);
            }
        }

        return new(seed.Id, seed.FullName, seed.ShortName, unitRows);
    }
}
