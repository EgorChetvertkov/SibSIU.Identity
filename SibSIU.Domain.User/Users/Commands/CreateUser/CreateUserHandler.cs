using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.CreateUser;
public sealed class CreateUserHandler(
    ILogger<CreateUserHandler> logger,
    IMemoryCache memory,
    IEmailSender emailService,
    MailPathForUserManager path,
    NavigationManager manager,
    AuthContext auth) : ICreateUserHandler
{
    public async Task<Result<Message>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));

        if (result.IsFailure)
        {
            return CreateResult.Failure<Message>(result.Error);
        }

        await EmailConfirmed.SendEmailConfirmationMail(emailService, path, manager, result.Data.Email, result.Data.UserId, cancellationToken);

        return CreateResult.Success(new Message("Пользователь создан. На указанный адрес отправлено письмо-подтверждение"));
    }

    private async Task<Result<CreateUserResult>> InnerHandler(CreateUserRequest request, CancellationToken cancellationToken)
    {
        bool userNameAlreadyExists = await auth.Users.AnyUserHasUserName(request.UserName, cancellationToken);
        if (userNameAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Имя пользователя {UserName} используется другим пользователем", request.UserName);
            return CreateResult.Failure<CreateUserResult>(UserErrors.UserNameAlreadyExists);
        }

        bool emailAlreadyExists = await auth.Users.AnyUserHasEmail(request.Email, cancellationToken);
        if (emailAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Адрес электронной почты {Email} используется другим пользователем", request.Email);
            return CreateResult.Failure<CreateUserResult>(UserErrors.EmailAlreadyExists);
        }

        bool phoneAlreadyExists = request.PhoneNumber != null &&
            await auth.Users.AnyUserHasPhone(request.PhoneNumber, cancellationToken);
        if (phoneAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Номер телефона {PhoneNumber} используется другим пользователем", request.PhoneNumber);
            return CreateResult.Failure<CreateUserResult>(UserErrors.PhoneAlreadyExists);
        }

        Gender? gender = await GetGender(request, cancellationToken);
        if (gender is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось получить пол по идентификатору {GenderId}", request.GenderId);
            return CreateResult.Failure<CreateUserResult>(UserErrors.GenderNotFound);
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
            IsConfirmedUser = true,
            IsTemporaryPassword = true,
        };

        await auth.Users.AddAsync(newUser, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new CreateUserResult(newUser.EmailConfirmed, newUser.Email, newUser.Id));
    }

    private async Task<Gender?> GetGender(CreateUserRequest request, CancellationToken cancellationToken)
    {
        string cacheKey = $"gender_{request.GenderId}";
        if (!memory.TryGetValue(cacheKey, out Gender? gender))
        {
            gender = await auth.Genders.GetGenderById(memory, request.GenderId, cancellationToken);

            MemoryCacheEntryOptions options = new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30),
            };

            memory.Set(cacheKey, gender, options);
        }

        return gender;
    }
}
