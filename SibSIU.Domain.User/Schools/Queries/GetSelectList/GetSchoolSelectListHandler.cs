using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Identity.Models.Schools;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetSelectList;
public sealed class GetSchoolSelectListHandler(
    ILogger<GetSchoolSelectListHandler> logger,
    AuthContext auth) : IGetSchoolSelectListHandler
{
    public async Task<List<SchoolItem>> Handle(GetSchoolSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<School> schools = auth.Schools.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            schools = schools.Where(s =>
                s.FullName.ToLower().Contains(request.Filter) ||
                s.ShortName.ToLower().Contains(request.Filter));
        }

        return await schools
            .Select(s => new SchoolItem(s.Id, s.FullName, s.ShortName))
            .ToListAsync(cancellationToken);
    }
}
