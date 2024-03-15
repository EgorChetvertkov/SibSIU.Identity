using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Identity.Models.Units;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Units.Queries.GetSelectList;
public sealed class GetUnitSelectListHandler(
    ILogger<GetUnitSelectListHandler> logger,
    AuthContext auth) : IGetUnitSelectListHandler
{
    public async Task<List<UnitItem>> Handle(GetUnitSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Unit> units = auth.Units.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            units = units.Where(u =>
                u.FullName.ToLower().Contains(request.Filter) ||
                u.ShortName.ToLower().Contains(request.Filter));
        }

        return await units
            .Select(u => new UnitItem(u.Id, u.FullName, u.ShortName))
            .ToListAsync(cancellationToken);
    }
}
