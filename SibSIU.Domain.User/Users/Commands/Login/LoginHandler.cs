using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.Names;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

using System.Security.Claims;

namespace SibSIU.Domain.UserManager.Users.Commands.Login;
public sealed class LoginHandler(
    ILogger<LoginHandler> logger,
    AuthContext auth) : ILoginHandler
{
    public Task<Result<LoginResult>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        return request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<LoginResult>> InnerHandler(LoginRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users
            .Where(u => u.UserName == request.UserName)
            .Include(u => u.Gender)
            .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
            .SingleOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Пользователя с именем {UserName} не обнаружено", request.UserName);
            return CreateResult.Failure<LoginResult>(UserErrors.UserNameNotFound);
        }

        bool isTruePassword = HashCalculator.CheckPassword(request.Password, user.Password, user.PasswordSalt);
        if (!isTruePassword)
        {
            auth.Rollback();
            logger.LogError("Пользователь с именем {UserName} указал не верный пароль", request.UserName);
            return CreateResult.Failure<LoginResult>(UserErrors.InvalidPassword);
        }

        string? message = GetMessage(user);
        ClaimsPrincipal principal = GetClaims(user);

        LoginResult result = string.IsNullOrWhiteSpace(message) ?
            LoginResult.GetSuccess(principal) :
            LoginResult.GetWarning(message, principal);
        return CreateResult.Success(result);
    }

    private static ClaimsPrincipal GetClaims(User user)
    {
        List<Claim> claims = [];
        claims.Add(new(ClaimNames.UserName, user.UserName));
        claims.Add(new(ClaimNames.Id, user.Id.ToString()));

        foreach (var item in user.UserRoles)
        {
            claims.Add(new(ClaimNames.Role, item.Role.Name));
        }

        if (user.BirthOfDate.HasValue)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            int age = now.Year - user.BirthOfDate.Value.Year;

            if (now.Month < user.BirthOfDate.Value.Month ||
               (now.Month == user.BirthOfDate.Value.Month && now.Day < user.BirthOfDate.Value.Day))
                age--;

            claims.Add(new(ClaimNames.Age, age.ToString()));
        }

        if (user.EmailConfirmed)
        {
            claims.Add(new(ClaimNames.EmailAddress, user.Email));
        }

        claims.Add(new(ClaimNames.FirstName, user.FirstName));
        claims.Add(new(ClaimNames.LastName, user.LastName));

        if (user.Gender is not null)
        {
            claims.Add(new(ClaimNames.Gender, user.Gender.Name));
        }

        if (user.Patronymic is not null)
        {
            claims.Add(new(ClaimNames.Patronymic, user.Patronymic));
        }

        return new(new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimNames.UserName, ClaimNames.Role));
    }

    private static string? GetMessage(User user)
    {
        string? message = null;
        if (!user.EmailConfirmed)
        {
            message = "Адрес электронной почты не подтвержден. Подтвердите адрес электронной почты чтобы получать уведомления";
        }
        if (user.IsTemporaryPassword)
        {
            message = "Пароль является временным. Смените его. Временные пароли являются небезопасными";
        }

        return message;
    }
}
