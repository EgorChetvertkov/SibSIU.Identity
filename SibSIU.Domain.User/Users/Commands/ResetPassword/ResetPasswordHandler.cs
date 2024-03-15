using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ResetPassword;
public sealed class ResetPasswordHandler(
    ILogger<ResetPasswordHandler> logger,
    AuthContext auth) : IResetPasswordHandler
{
    public async Task<Result<Message>> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        if (!PasswordReset.IsEqualCode(request.Code, user.Password, user.Id.ToString()))
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.InvalidConfirmationCode);
        }

        HashResult hashResult = HashCalculator.Hash(request.NewPassword);

        user.Password = hashResult.Password;
        user.PasswordSalt = hashResult.Salt;
        user.IsTemporaryPassword = false;
        user.UpdateAt = DateTimeOffset.UtcNow;

        return CreateResult.Success(new Message("Пароль успешно изменен"));
    }
}
