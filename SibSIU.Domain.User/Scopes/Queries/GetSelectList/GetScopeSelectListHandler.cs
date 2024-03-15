using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetSelectList;
public sealed class GetScopeSelectListHandler(
    ILogger<GetScopeSelectListHandler> logger,
    AuthContext auth) : IGetScopeSelectListHandler
{
    public async Task<List<ScopeItem>> Handle(GetScopeSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Scope> scopes = auth.Scopes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            scopes = scopes.Where(ct =>
                ct.Name.ToLower().Contains(request.Filter));
        }

        return await scopes
            .Select(s => new ScopeItem(s.Id, s.Name))
            .ToListAsync(cancellationToken);
    }
}
