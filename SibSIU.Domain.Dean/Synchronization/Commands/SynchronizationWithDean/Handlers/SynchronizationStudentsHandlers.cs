using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;
internal static class SynchronizationStudentsHandlers
{
    public static async Task Synchronization(
        AuthContext identity,
        DeanContext dean,
        ILogger<SynchronizationWithDeanHandler> logger,
        CancellationToken cancellationToken)
    {
        List<TempStudentDeanCodeFromDeanDB> deanStudents = await SelectDeanStudents(identity, dean, cancellationToken);

        List<Student> existsStudents = await SelectExists(identity, cancellationToken);

        if (existsStudents.Count == 0)
        {
            return;
        }

        List<TempStudentDeanCodeFromDeanDB> ownDeanGroup = existsStudents
            .Select(s => new TempStudentDeanCodeFromDeanDB(s.DeanCode, s.User.FirstName, s.User.LastName, s.User.Patronymic, s.Group.Name, s.Rank, s.User.BirthOfDate ?? DateTimeOffset.MinValue))
            .ToList();

        List<TempStudentDeanCodeFromDeanDB> forDelete = ownDeanGroup.Except(deanStudents).ToList();

        await Update(deanStudents, existsStudents, identity, cancellationToken);

        await Delete(forDelete, identity, cancellationToken);

        _ = await identity.SaveChangesAsync(cancellationToken);
    }

    private static async Task<List<TempStudentDeanCodeFromDeanDB>> SelectDeanStudents(AuthContext identity, DeanContext dean, CancellationToken cancellationToken)
    {
        _ = await identity.TempStudents.ExecuteDeleteAsync(cancellationToken);
        List<TempStudentDeanCodeFromDeanDB> students = await SelectTempStudentsFromDean(dean, cancellationToken);

        await identity.TempStudents.AddRangeAsync(students, cancellationToken);
        _ = await identity.SaveChangesAsync(cancellationToken);

        var deanStudents = await identity.TempStudents
            .AsNoTracking()
            .Join(identity.Students, ts => ts.DeanCode, s => s.DeanCode,
            (ts, s) => ts)
            .ToListAsync(cancellationToken);

        _ = await identity.TempStudents.ExecuteDeleteAsync(cancellationToken);

        return deanStudents;
    }

    private static async Task<List<Student>> SelectExists(AuthContext identity, CancellationToken cancellationToken)
    {
        return await identity.Students
            .IgnoreQueryFilters()
            .Include(s => s.User)
            .Include(s => s.Group)
            .ToListAsync(cancellationToken);
    }

    private static async Task Delete(List<TempStudentDeanCodeFromDeanDB> forDelete, AuthContext identity, CancellationToken cancellationToken)
    {
        if (forDelete.Count > 0)
        {
            var deletingIds = forDelete.Select(u => u.DeanCode).ToList();

            DateTime now = DateTime.UtcNow;

            await identity.Students
                .Where(u => deletingIds.Contains(u.DeanCode))
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.IsActive, false)
                    .SetProperty(u => u.UpdateAt, now),
                    cancellationToken);
            _ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
    }

    private static async Task Update(List<TempStudentDeanCodeFromDeanDB> deanStudents, List<Student> existsStudents, AuthContext identity, CancellationToken cancellationToken)
    {
        Dictionary<string, AcademicGroup?> groups = [];
        foreach (var own in existsStudents)
        {
            var dean = deanStudents.FirstOrDefault(i => i.DeanCode == own.DeanCode);
            if (dean is null)
            {
                continue;
            }

            if(!groups.TryGetValue(dean.GroupName, out AcademicGroup? group))
            {
                group = await identity.GetGroup(dean.GroupName, cancellationToken);
                groups.Add(dean.GroupName, group);
            }

            if (group == default)
            {
                continue;
            }

            own.Rank = dean.Rank;
            own.Group = group;
            own.UpdateAt = DateTime.UtcNow;
            own.IsActive = true;
            own.User.FirstName = dean.FirstName;
            own.User.LastName = dean.LastName;
            own.User.Patronymic = dean.Patronymic;
            own.User.BirthOfDate = dean.Birthday;
            own.User.UpdateAt = DateTime.UtcNow;
            own.User.IsActive = true;
        }

        _ = await identity.SaveChangesAsync(cancellationToken);
        identity.ChangeTracker.Clear();
    }

    private static async Task<List<TempStudentDeanCodeFromDeanDB>> SelectTempStudentsFromDean(DeanContext dean, CancellationToken cancellationToken)
    {
        return await dean.ВсеСтудентыs
            .Where(s => s.Статус == 1 && s.ДатаРождения != null && s.КодГруппы != null)
            .Join(dean.ВсеГруппыs, s => s.КодГруппы, g => g.Код,
            (s, g) => new { DeanCode = s.Код, FirstName = s.Имя, LastNasme = s.Фамилия, Patronymic = s.Отчество, Birthday = s.ДатаРождения, GroupName = g.Название })
            .GroupJoin(dean.Оценкиs, s => s.DeanCode, m => m.КодСтудента, (s, m) => new { s, m })
            .SelectMany(x => x.m.DefaultIfEmpty(),
                (s, m) => new
                {
                    s.s.DeanCode,
                    s.s.FirstName,
                    s.s.LastNasme,
                    s.s.Patronymic,
                    s.s.GroupName,
                    s.s.Birthday,
                    v = m == null ? 0 : m.КодВедомости,
                    Mark = m == null ? 0 : m.ИтоговаяОценка
                })
            .GroupJoin(dean.ВсеВедомостиs, m => m.v, v => v.Код, (m, v) => new { m, v })
            .SelectMany(x => x.v.DefaultIfEmpty(),
            (m, v) => new
            {
                m.m.DeanCode,
                m.m.FirstName,
                m.m.LastNasme,
                m.m.Patronymic,
                m.m.GroupName,
                m.m.Birthday,
                m.m.Mark,
                IsClose = v == null ? false : v.Закрыта
            })
            .Where(s => 0 <= s.Mark && s.Mark <= 5 && s.IsClose == true)
            .GroupBy(s => new { s.DeanCode, s.FirstName, s.LastNasme, s.Patronymic, s.GroupName, s.Birthday })
            .Select(s => new TempStudentDeanCodeFromDeanDB(s.Key.DeanCode, s.Key.FirstName, s.Key.LastNasme, s.Key.Patronymic, s.Key.GroupName, s.Average(s => s.Mark!.Value), s.Key.Birthday!.Value))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
