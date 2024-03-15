using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Queries.GetSelectList;
public class GetUserSelectListHandler(
    ILogger<GetUserSelectListHandler> logger,
    AuthContext auth) : IGetUserSelectListHandler
{
    public async Task<List<UserItem>> Handle(GetUserSelectListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<User> users = auth.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            users = users.Where(u =>
                u.FirstName.ToLower().Contains(request.Filter) ||
                u.LastName.ToLower().Contains(request.Filter) ||
                (u.Patronymic != null && u.Patronymic.ToLower().Contains(request.Filter)));
        }

        return await users
            .Select(u => new UserItem(u.Id, u.FirstName, u.LastName, u.Patronymic))
            .ToListAsync(cancellationToken);
    }
}
