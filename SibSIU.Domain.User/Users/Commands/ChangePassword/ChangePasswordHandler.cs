using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Authenticate.Password;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.MailService.Mailer.Base;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;
using SibSIU.Domain.SendMail.MailModels;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePassword;
public sealed class ChangePasswordHandler(
    ILogger<ChangePasswordHandler> logger,
    MailPathForUserManager path,
    IEmailSender emailService,
    AuthContext auth) : IChangePasswordHandler
{
    public async Task<Result<Message>> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        Result<ChangePasswordResult> result = await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));

        if (result.IsFailure)
        {
            return CreateResult.Failure<Message>(result.Error);
        }

        if (result.Data.EmailConfirmed)
        {
            Dictionary<string, InLineAttachment> inLine = [];
            inLine.Add("logo", new("Logo_image", path.LogoPath));

            await emailService.SendMailWithTemplate(
                new MailMessageWithTemplate<MailPasswordChangeModel>(
                    "Изменение учетных данных",
                    result.Data.Email,
                    path.ChangePasswordTemplatePath,
                    new MailPasswordChangeModel(request.NewPassword,
                    inLine)),
                [],
                cancellationToken);
        }

        return CreateResult.Success(new Message("Пароль успешно изменен"));
    }

    private async Task<Result<ChangePasswordResult>> InnerHandle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Пользователь {UserId} не найден", request.UserId);
            return CreateResult.Failure<ChangePasswordResult>(UserErrors.UserNotFound);
        }

        bool isTruePassword = HashCalculator.CheckPassword(request.OldPassword, user.Password, user.PasswordSalt);
        if (!isTruePassword)
        {
            auth.Rollback();
            logger.LogError("Пользователь {UserId} указал не верный пароль", request.UserId);
            return CreateResult.Failure<ChangePasswordResult>(UserErrors.InvalidPassword);
        }

        HashResult hashResult = HashCalculator.Hash(request.NewPassword);

        user.Password = hashResult.Password;
        user.PasswordSalt = hashResult.Salt;
        user.IsTemporaryPassword = false;
        user.UpdateAt = DateTimeOffset.UtcNow;

        return CreateResult.Success(new ChangePasswordResult(user.EmailConfirmed, user.Email));
    }
}
