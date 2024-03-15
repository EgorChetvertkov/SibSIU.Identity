using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsPupil;
public class RegisterAsPupilHandler(
    ILogger<RegisterAsPupilHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IRegisterAsPupilHandler
{
    public async Task<Result<Message>> Handle(RegisterAsPupilRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(RegisterAsPupilRequest request, CancellationToken cancellationToken)
    {
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

        School? school = await auth.Schools.GetById(request.SchoolId, cancellationToken);
        if (school is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось получить школу по идентификатору {SchoolId}", request.SchoolId);
            return CreateResult.Failure<Message>(SchoolErrors.SchoolNotFound);
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
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber is null ? null : PhoneValidation.ReturnOnlyDigits(request.PhoneNumber),
            Gender = gender,
            Password = hashPassword.Password,
            PasswordSalt = hashPassword.Salt,
            IsConfirmedUser = false,
            IsTemporaryPassword = false,
        };

        Pupil pupilUser = new()
        {
            CreateAt = now,
            UpdateAt = now,
            Id = Ulid.NewUlid(now),
            UserId = newUser.Id,
            IsActive = true,
            ClassLitter = request.ClassLitter,
            ClassNumber = request.ClassNumber,
            SchoolId = request.SchoolId,
        };

        await auth.Users.AddAsync(newUser, cancellationToken);
        await auth.Pupils.AddAsync(pupilUser, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Заявка успешно подана"));
    }
}
