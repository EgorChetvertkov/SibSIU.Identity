using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;
public sealed class ChangeEmailHandler(
    ILogger<ChangeEmailHandler> logger,
    IEmailSender emailService,
    MailPathForUserManager path,
    NavigationManager manager,
    AuthContext auth) : IChangeEmailHandler
{
    public async Task<Result<Message>> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));

        if (result.IsFailure)
        {
            return CreateResult.Failure<Message>(result.Error);
        }

        await EmailConfirmed.SendEmailConfirmationMail(emailService, path, manager, result.Data.Email, result.Data.UserId, cancellationToken);

        return CreateResult.Success(new Message("Электронная почта изменена. На указанный адрес отправлено письмо-подтверждение"));
    }

    private async Task<Result<ChangeEmailResult>> InnerHandle(ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось обнаружить пользователя {UserId}", request.UserId);
            return CreateResult.Failure<ChangeEmailResult>(UserErrors.UserNotFound);
        }

        bool emailAlreadyExists = await auth.Users.AnyUserHasEmail(request.Email, cancellationToken);
        if (emailAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Адрес электронной почты {Email} используется другим пользователем", request.Email);
            return CreateResult.Failure<ChangeEmailResult>(UserErrors.EmailAlreadyExists);
        }

        user.Email = request.Email;
        user.EmailConfirmed = false;
        user.UpdateAt = DateTimeOffset.UtcNow;

        return CreateResult.Success(new ChangeEmailResult(user.EmailConfirmed, user.Email, user.Id));
    }
}
