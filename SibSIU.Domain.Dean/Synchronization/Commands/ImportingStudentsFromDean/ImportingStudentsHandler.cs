using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
using SibSIU.Identity.Models.User.Students;
using SibSIU.UserData.Database.Entities;

using System.Text;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean;
public sealed class ImportingStudentsHandler(
    IMemoryCache memory,
    ILogger<ImportingStudentsHandler> logger,
    DeanContext dean,
    AuthContext auth) : IImportingStudentsHandler
{
    private readonly Dictionary<string, AcademicGroup> _academicGroups = [];

    public async Task<Result<ComparativeUserBeforeImportList>> Handle(
        ImportingStudentsRequest request,
        CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await InnerHandler(request, cancellationToken));
    }

    private async Task<Result<ComparativeUserBeforeImportList>> InnerHandler(ImportingStudentsRequest request, CancellationToken cancellationToken)
    {
        List<DeanStudentInfo> students = await SelectNotExistsStudnetsByGroupsDeanCode(request.GetGroupNames(), cancellationToken);
        List<PlainStudentData> plainStudents = await GetPlainStudentData(students, cancellationToken);

        string cacheKey = Cached(plainStudents);

        ComparativeUserBeforeImportList response = await GetSimilarUsers(plainStudents, cacheKey, cancellationToken);

        return CreateResult.Success(response);
    }

    private string Cached(List<PlainStudentData> plainStudents)
    {
        string cacheKey = Ulid.NewUlid(DateTimeOffset.Now).ToString();

        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(60)
        };
        memory.Set<List<PlainStudentData>>(cacheKey, plainStudents, options);
        return cacheKey;
    }

    private async Task<ComparativeUserBeforeImportList> GetSimilarUsers(List<PlainStudentData> plainStudents, string cacheKey, CancellationToken cancellationToken)
    {
        List<ComparativeUserBeforeImport> data = [];

        foreach (var student in plainStudents)
        {
            var similarStudents = await auth.Users
                .Where(u =>
                    u.FirstName == student.FirstName &&
                    u.LastName == student.LastName &&
                    u.Patronymic == student.Patronymic)
                .Include(u => u.Gender)
                .Include(u => u.Students)
                    .ThenInclude(s => s.Group)
                .Select(u => new UserWithStudentDisplay(
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.PhoneNumber ?? string.Empty,
                    u.FirstName,
                    u.LastName,
                    u.Patronymic ?? string.Empty,
                    u.BirthOfDate.GetValueOrDefault(),
                    u.Gender == null ? string.Empty : u.Gender.Name,
                    u.Students.Select(s => new StudentInfo(s.DeanCode, s.Group.Name, s.Rank)).ToList()))
                .ToListAsync(cancellationToken);

            if(similarStudents.Count > 0)
            {
                data.Add(new(
                    student.GetUserWithStudentDisplay(),
                    similarStudents
                    .Select(s => new ExistsSimilarUserWithStudentDisplay(s, false))
                    .ToList()));
            }
        }

        ComparativeUserBeforeImportList response = new(cacheKey, data);
        return response;
    }

    private async Task<List<DeanStudentInfo>> SelectNotExistsStudnetsByGroupsDeanCode(List<string> groups, CancellationToken cancellationToken)
    {
        _ = await auth.TempStudents.ExecuteDeleteAsync(cancellationToken);

        List<TempStudentDeanCodeFromDeanDB> students = await GetAllStudents(groups, cancellationToken);

        await auth.TempStudents.AddRangeAsync(students, cancellationToken);
        _ = await auth.SaveChangesAsync(cancellationToken);

        List<DeanStudentInfo> nonExistsStudents = await GetNonExistsStudents(cancellationToken);

        _ = await auth.TempStudents.ExecuteDeleteAsync(cancellationToken);

        return nonExistsStudents;
    }

    private async Task<List<TempStudentDeanCodeFromDeanDB>> GetAllStudents(List<string> groups, CancellationToken cancellationToken)
    {
        return await dean.ВсеГруппыs
            .Where(g => groups.Contains(g.Название))
            .Join(dean.ВсеСтудентыs.Where(s => s.Статус == 1 && s.ДатаРождения != null),
            g => g.Код,
            s => s.КодГруппы,
            (g, s) => new { DeanCode = s.Код, FirstName = s.Имя, LastNasme = s.Фамилия, Patronymic = s.Отчество, GroupName = g.Название, Birthday = s.ДатаРождения })
            .GroupJoin(dean.Оценкиs.Where(m => m.ИтоговаяОценка >= 0 && m.ИтоговаяОценка <= 5), s => s.DeanCode, m => m.КодСтудента,
            (s, m) => new { s, m })
            .SelectMany(x => x.m.DefaultIfEmpty(),
              (s, m) => new
              {
                  s.s.DeanCode,
                  s.s.FirstName,
                  s.s.LastNasme,
                  s.s.Patronymic,
                  s.s.GroupName,
                  s.s.Birthday,
                  Mark = m == null ? 0 : m.ИтоговаяОценка,
                  v = m == null ? 0 : m.КодВедомости
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
            .ToListAsync(cancellationToken);
    }

    private async Task<List<DeanStudentInfo>> GetNonExistsStudents(CancellationToken cancellationToken)
    {
        return await auth.TempStudents
          .GroupJoin(auth.Students, ts => ts.DeanCode, s => s.DeanCode,
          (ts, s) => new { ts, s })
          .SelectMany(tss => tss.s.DefaultIfEmpty(),
          (tss, s) => new { tss.ts, s })
          .Where(tss => tss.s == null)
          .Select(tss => tss.ts)
                  .OrderBy(ts => ts.GroupName).ThenBy(s => s.LastName)
          .Join(auth.AcademicGroups, ts => ts.GroupName, g => g.Name,
          (ts, g) => new DeanStudentInfo(ts.FirstName, ts.LastName, ts.Patronymic, ts.DeanCode, g.Name, ts.Birthday, ts.Rank))
          .ToListAsync(cancellationToken);
    }

    private async Task<List<PlainStudentData>> GetPlainStudentData(List<DeanStudentInfo> students, CancellationToken cancellationToken)
    {
        List<PlainStudentData> users = [];
        foreach (DeanStudentInfo student in students)
        {
            string fullName = GetFullName(student);
            int countStartWithUserNames = await auth.Users
                .Where(u => u.UserName.StartsWith(fullName))
                .CountAsync(cancellationToken);

            UserNameAndPassword userNameAndPassword = UserNameAndPasswordGenerator.ByFullName(fullName, countStartWithUserNames);

            var group = await GetGroupByName(student.GroupName);

            if (group is null)
            {
                logger.LogError("{FullName} has not group with dean code {GroupDeanCode}", student.FullName, student.GroupName);
                continue;
            }

            users.Add(new(
                Ulid.NewUlid(),
                student.FirstName,
                student.LastName,
                student.Patronymic,
                userNameAndPassword.UserName,
                userNameAndPassword.Password,
                userNameAndPassword.EmailAddress,
                student.Birthday,
                student.DeanCode,
                student.Rank,
                group));
        }

        return users;
    }

    private static string GetFullName(DeanStudentInfo student)
    {
        StringBuilder sb = new();
        _ = sb.Append(student.LastName).Append(student.FirstName);

        if (!string.IsNullOrWhiteSpace(student.Patronymic))
        {
            _ = sb.Append(student.Patronymic);
        }

        string fullName = sb.ToString();
        return fullName;
    }

    private async Task<AcademicGroup?> GetGroupByName(string groupName)
    {
        var group = _academicGroups.GetValueOrDefault(groupName);

        if (group is null)
        {
            group = await auth.AcademicGroups
                .Where(g => g.Name.ToLower() == groupName.ToLower())
                .SingleOrDefaultAsync();

            if (group is not null)
            {
                _academicGroups.Add(groupName, group);
            }
        }

        return group;
    }
}
