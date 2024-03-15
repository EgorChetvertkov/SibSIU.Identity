using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Dean.Database;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsStudent;
public sealed class RegisterAsStudentHandler(
    ILogger<RegisterAsStudentHandler> logger,
    IMemoryCache memory,
    AuthContext auth,
    DeanContext dean) : IRegisterAsStudentHandler
{
    public async Task<Result<Message>> Handle(RegisterAsStudentRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(RegisterAsStudentRequest request, CancellationToken cancellationToken)
    {
        bool userWithDeanAlreadyExists = await auth.Students
            .Where(s => s.DeanCode == request.DeanCode)
            .AnyAsync(cancellationToken);
        if (userWithDeanAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Код {DeanCode} из системы Деканат уже используется", request.DeanCode);
            return CreateResult.Failure<Message>(StudentErrors.DeanCodeAlreadyUse);
        }

        var studentData = await GetStudentAsync(request.DeanCode, cancellationToken);
        if (studentData is null)
        {
            auth.Rollback();
            logger.LogError("В Деканате не существует обучающегося с идентификатором {DeanCode}", request.DeanCode);
            return CreateResult.Failure<Message>(StudentErrors.InvalidStudentDeanCode);
        }

        if (studentData.Birthday.Date != request.BirthOfDate.Date)
        {
            auth.Rollback();
            logger.LogError("Дата рождения не совпадает с датой рождения указанной в системе Деканат");
            return CreateResult.Failure<Message>(StudentErrors.BirthdayMastBeEqualsWithDean);
        }

        bool userNameAlreadyExists = await auth.Users.AnyUserHasUserName(request.UserName, cancellationToken);
        if (userNameAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Имя пользователя {UserName} используется другим пользователем", request.UserName);
            return CreateResult.Failure<Message>(UserErrors.UserNameAlreadyExists);
        }

        bool emailAlreadyExists = await auth.Users.AnyUserHasEmail(request.Email, cancellationToken);
        if (emailAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Адрес электронной почты {Email} используется другим пользователем", request.Email);
            return CreateResult.Failure<Message>(UserErrors.EmailAlreadyExists);
        }

        bool phoneAlreadyExists = request.PhoneNumber != null &&
            await auth.Users.AnyUserHasPhone(request.PhoneNumber, cancellationToken);
        if (phoneAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Номер телефона {PhoneNumber} используется другим пользователем", request.PhoneNumber);
            return CreateResult.Failure<Message>(UserErrors.PhoneAlreadyExists);
        }

        Gender? gender = await auth.Genders.GetGenderById(memory, request.GenderId, cancellationToken);
        if (gender is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось получить пол по идентификатору {GenderId}", request.GenderId);
            return CreateResult.Failure<Message>(UserErrors.GenderNotFound);
        }

        AcademicGroup? group = await auth.AcademicGroups.GetByName(studentData.GroupName, cancellationToken);
        if (group is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось получить группу по имени {GroupName}", studentData.GroupName);
            return CreateResult.Failure<Message>(StudentErrors.GroupNotFound);
        }

        HashResult hashPassword = HashCalculator.Hash(request.Password);

        DateTimeOffset now = DateTimeOffset.UtcNow;
        User newUser = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            BirthOfDate = request.BirthOfDate.Date,
            Email = request.Email,
            EmailConfirmed = false,
            FirstName = studentData.FirstName,
            LastName = studentData.LastName,
            Patronymic = studentData.Patronymic,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber is null ? null : PhoneValidation.ReturnOnlyDigits(request.PhoneNumber),
            Gender = gender,
            Password = hashPassword.Password,
            PasswordSalt = hashPassword.Salt,
            IsConfirmedUser = false,
            IsTemporaryPassword = false,
        };

        Student studentUser = new()
        {
            CreateAt = now,
            UpdateAt = now,
            Id = Ulid.NewUlid(now),
            UserId = newUser.Id,
            IsActive = true,
            DeanCode = request.DeanCode,
            Rank = studentData.Rank,
            AcademicGroupId = group.Id,
        };

        await auth.Users.AddAsync(newUser, cancellationToken);
        await auth.Students.AddAsync(studentUser, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Заявка успешно подана"));
    }

    private async Task<TempStudentDeanCodeFromDeanDB?> GetStudentAsync(int deanCode, CancellationToken cancellationToken)
    {
        return await dean.ВсеСтудентыs
            .Where(s => s.Код == deanCode && s.Статус == 1 && s.ДатаРождения != null)
            .Join(dean.ВсеГруппыs,
            g => g.КодГруппы,
            s => s.Код,
            (s, g) => new { DeanCode = s.Код, FirstName = s.Имя, LastNasme = s.Фамилия, Patronymic = s.Отчество, GroupName = g.Название, Birthday = s.ДатаРождения })
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
            .SingleOrDefaultAsync(cancellationToken);
    }
}
