using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Dean.Database;
using SibSIU.Identity.Models.User.Students;

namespace SibSIU.Domain.UserManager.Users.Queries.GetStudentsWithoutUser;
public sealed class GetStudentsWithoutUserHandler(
    ILogger<GetStudentsWithoutUserHandler> logger,
    AuthContext auth,
    DeanContext dean) : IGetStudentsWithoutUserHandler
{
    public async Task<List<StudentItem>> Handle(GetStudentsWithoutUserRequest request, CancellationToken cancellationToken)
    {
        var list = await GetStudentsFromDean(request.GroupName, cancellationToken);
        var existsList = await GetStudentsFromAuth(request.GroupName, cancellationToken);
        return list.Except(existsList).ToList();
    }

    private async Task<List<StudentItem>> GetStudentsFromDean(string groupName, CancellationToken cancellationToken)
    {
        return await dean.ВсеГруппыs
            .Where(g => g.Название.ToLower() == groupName.ToLower())
            .Join(dean.ВсеСтудентыs, g => g.Код, s => s.КодГруппы,
            (g, s) => s)
            .Where(s => s.Статус == 1 && s.ДатаРождения != null)
            .Select(s => new StudentItem(s.Код, s.Имя, s.Фамилия, s.Отчество))
            .ToListAsync(cancellationToken);
    }

    private async Task<List<StudentItem>> GetStudentsFromAuth(string groupName, CancellationToken cancellationToken)
    {
        return await auth.AcademicGroups
            .Where(g => g.Name.ToLower() == groupName.ToLower())
            .SelectMany(g => g.Students)
            .Select(s => new StudentItem(s.DeanCode, s.User.FirstName, s.User.LastName, s.User.Patronymic))
            .ToListAsync(cancellationToken);
    }
}
