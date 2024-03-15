using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
using SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents.Models;
using SibSIU.Identity.Models.User.Imports;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents;
public sealed class SaveStudentsHandler(
    IMemoryCache memory,
    ILogger<SaveStudentsHandler> logger,
    AuthContext auth) : ISaveStudentsHandler
{
    private readonly Dictionary<string, AcademicGroup> _academicGroups = [];

    public async Task<Result<CsvLoginData>> Handle(SaveStudentsRequest request, CancellationToken cancellationToken)
    {
        List<PlainStudentData>? plainStudents = memory.Get<List<PlainStudentData>>(request.CacheKey);
        if (plainStudents is null)
        {
            return CreateResult.Failure<CsvLoginData>(Error.NotFound("Записи об обучающихся не удалось восстановить. Повторите попытку"));
        }

        var updated = request.MustBeUpdate;
        List<CreateStudent> inserted = GetInsertedStudents(plainStudents, updated);

        return await auth.WithTransaction(logger, request, async (request) =>
        {
            await InsertNewUsersAsync(inserted, cancellationToken);
            await InsertStudentInfoForExistsUsersAsync(updated, cancellationToken);
            CsvLoginData csv = new(DomainCsvHandler
                .WriteCsvByList(plainStudents
                .Select(s => s.GetCsvStudentListItem())
                .ToList()));
            return CreateResult.Success(csv);
        });
    }

    private static List<CreateStudent> GetInsertedStudents(List<PlainStudentData> plainStudents, List<AddAcademicGroup> updated)
    {
        List<CreateStudent> inserted = [];
        List<Ulid> onlyUpdate = updated.Select(u => u.NewUserId).ToList();
        foreach (var plainStudent in plainStudents)
        {
            if (!onlyUpdate.Contains(plainStudent.Id))
            {
                inserted.Add(plainStudent.GetCreateStudent());
            }
        }

        return inserted;
    }

    private async Task InsertStudentInfoForExistsUsersAsync(List<AddAcademicGroup> updated, CancellationToken cancellationToken)
    {
        if(updated.Count == 0)
        {
            return;
        }

        List<Student> students = [];
        foreach (var update in updated)
        {
            foreach (var item in update.NewStudentData)
            {
                var group = await GetGroupByName(item.GroupName);
                if (group is null)
                {
                    logger.LogError("Группа {group} не найдена", group);
                    continue;
                }

                DateTimeOffset now = DateTimeOffset.UtcNow;
                students.Add(new()
                {
                    Id = Ulid.NewUlid(now),
                    DeanCode = item.StudentDeanCode,
                    CreateAt = now,
                    UpdateAt = now,
                    IsActive = true,
                    UserId = update.ExistsUserId,
                    Rank = item.Rank,
                    AcademicGroupId = group.Id,
                });
            }
        }

        await auth.Students.AddRangeAsync(students, cancellationToken);
    }

    private async Task InsertNewUsersAsync(List<CreateStudent> create, CancellationToken cancellationToken)
    {
        if(create.Count == 0)
        {
            return;
        }
        
        List<User> users = [];
        List<Student> students = [];
        User createdUser;
        DateTimeOffset now;
        foreach (var user in create)
        {
            now = DateTimeOffset.UtcNow;
            createdUser = new()
            {
                CreateAt = now,
                UpdateAt = now,
                IsActive = true,
                Id = user.Id,
                Email = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                UserName = user.UserName,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt,
                BirthOfDate = user.Birthday,
                EmailConfirmed = false,
                PhoneNumber = string.Empty,
                IsConfirmedUser = true,
                IsTemporaryPassword = true,
            };

            now = DateTimeOffset.UtcNow;
            Student student = new()
            {
                DeanCode = user.DeanCode,
                User = createdUser,
                CreateAt = now,
                UpdateAt = now,
                Id = Ulid.NewUlid(now),
                UserId = user.Id,
                IsActive = true,
                Rank = user.Rank,
                AcademicGroupId = user.AcademicGroup.Id,
            };

            users.Add(createdUser);
            students.Add(student);
        }

        await auth.Users.AddRangeAsync(users, cancellationToken);
        await auth.Students.AddRangeAsync(students, cancellationToken);
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
