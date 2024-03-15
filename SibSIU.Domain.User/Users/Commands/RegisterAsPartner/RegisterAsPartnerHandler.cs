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

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsPartner;
public sealed class RegisterAsPartnerHandler(
    ILogger<RegisterAsPartnerHandler> logger,
    AuthContext auth,
    IMemoryCache memory) : IRegisterAsPartnerHandler
{
    public async Task<Result<Message>> Handle(RegisterAsPartnerRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(RegisterAsPartnerRequest request, CancellationToken cancellationToken)
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

        Post? post = await auth.Posts.GetById(request.PostId, cancellationToken);
        if (post is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось получить должность по идентификатору {PostId}", request.PostId);
            return CreateResult.Failure<Message>(PostErrors.PostNotFound);
        }

        Organization? organization = await auth.Organizations.GetById(request.OrganizationId, cancellationToken);
        if (organization is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось получить организацию по идентификатору {OrganizationId}", request.OrganizationId);
            return CreateResult.Failure<Message>(OrganizationErrors.OrganizationNotFound);
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

        Partner partnerUser = new()
        {
            CreateAt = now,
            UpdateAt = now,
            Id = Ulid.NewUlid(now),
            UserId = newUser.Id,
            IsActive = true,
            OrganizationId = request.OrganizationId,
            PostId = request.PostId,
        };

        await auth.Users.AddAsync(newUser, cancellationToken);
        await auth.Partners.AddAsync(partnerUser, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Заявка успешно подана"));
    }
}
