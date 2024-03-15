using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetSelectList;
public sealed class GetClaimTypeSelectListHandler(
    ILogger<GetClaimTypeSelectListHandler> logger,
    AuthContext auth) : IGetClaimTypeSelectListHandler
{
    public async Task<List<ClaimTypeItem>> Handle(GetClaimTypeSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<AuthClaimType> claimTypes = auth.ClaimTypes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            claimTypes = claimTypes.Where(ct =>
                ct.Name.ToLower().Contains(request.Filter));
        }

        return await claimTypes
            .Select(s => new ClaimTypeItem(s.Id, s.Name))
            .ToListAsync(cancellationToken);
    }
}
