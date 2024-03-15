using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Dean.Database;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.AddStudent;
public sealed class AddStudentHandler(
    ILogger<AddStudentHandler> logger,
    DeanContext dean,
    AuthContext auth) : IAddStudentHandler
{
    public async Task<Result<Message>> Handle(AddStudentRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(AddStudentRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetUserByIdWithStudents(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        int countStudents = await auth.Students
            .Where(s => s.UserId == request.UserId)
            .CountAsync(cancellationToken);
        if (countStudents != 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.CountActiveStudentsMustBeZeroForAddNew);
        }

        Student? deanStudent = await GetDeanStudent(dean, request, cancellationToken);
        if (deanStudent is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(StudentErrors.InvalidStudentDeanCode);
        }

        if (user.Students.Select(s => s.Group.Name).Any(g => g == deanStudent.Group.Name))
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(Error.Conflict($"Пользователь {user.FullName()} уже является студентом группы {deanStudent.Group.Name}."));
        }

        AcademicGroup? group = await auth.AcademicGroups.GetByName(deanStudent.Group.Name, cancellationToken);
        if (group is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(StudentErrors.GroupNotFound);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Student student = new()
        {
            Id = Ulid.NewUlid(now),
            IsActive = true,
            CreateAt = now,
            UpdateAt = now,
            Group = group,
            User = user,
            DeanCode = deanStudent.DeanCode,
            Rank = 0,
        };
        await auth.Students.AddAsync(student, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message($"Пользователь {user.FullName()} является студентом группы {deanStudent.Group.Name}. В процессе синхронизации с системой Деканат, в случае, если пользователь не является обучающимся по данным системы Деканат, данное изменение будет отменено"));
    }

    private static async Task<Student?> GetDeanStudent(DeanContext dean, AddStudentRequest request, CancellationToken cancellationToken)
    {
        return await dean.ВсеСтудентыs
            .Where(s => s.Код == request.DeanCode)
            .Join(dean.ВсеГруппыs,
            s => s.КодГруппы, g => g.Код,
            (s, g) => new Student()
            {
                DeanCode = s.Код,
                Rank = 0,
                Group = new()
                {
                    Name = g.Название,
                    StartYear = g.Курс ?? -1,
                }
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}
