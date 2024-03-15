using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database;
using SibSIU.Identity.Models.AcademicGroups;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Groups.Queries.GetSelectList;
public sealed class GetGroupSelectListHandler(AuthContext auth) : IGetGroupSelectListHandler
{
    public async Task<List<AcademicGroupItem>> Handle(GetGroupSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<AcademicGroup> groups = auth.AcademicGroups.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            groups = groups.Where(o => o.Name.ToLower().Contains(request.Filter));
        }

        return await groups
            .Select(g => new AcademicGroupItem(g.Name))
            .ToListAsync(cancellationToken);
    }
}
