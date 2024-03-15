using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database;
using SibSIU.Identity.Models.Organizations;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Organizations.Queries.GetSelectList;
public sealed class GetOrganizationSelectListHandler(AuthContext auth) : IGetOrganizationSelectListHandler
{
    public async Task<List<OrganizationItem>> Handle(GetOrganizationSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Organization> organizations = auth.Organizations.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            organizations = organizations.Where(o => o.FullName.ToLower().Contains(request.Filter));
        }

        return await organizations
            .Select(o => new OrganizationItem(o.Id, o.FullName, o.ShortName))
            .ToListAsync(cancellationToken);
    }
}
