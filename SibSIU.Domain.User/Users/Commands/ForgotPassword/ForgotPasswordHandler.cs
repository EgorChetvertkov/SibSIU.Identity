using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.MailService.Mailer.Base;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.SendMail.MailModels;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ForgotPassword;
public sealed class ForgotPasswordHandler(
    ILogger<ForgotPasswordHandler> logger,
    IEmailSender emailService,
    MailPathForUserManager path,
    NavigationManager manager,
    AuthContext auth) : IForgotPasswordHandler
{
    public async Task<Result<Message>> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));

        if (result.IsFailure)
        {
            return CreateResult.Failure<Message>(result.Error);
        }

        string code = PasswordReset.Hash(result.Data.Password, result.Data.UserId);
        var callbackUrl = manager.GetUriWithQueryParameters(
        manager.ToAbsoluteUri("reset_password").AbsoluteUri,
            new Dictionary<string, object?> { ["userId"] = result.Data.UserId, ["code"] = code });

        Dictionary<string, InLineAttachment> inLine = [];
        inLine.Add("logo", new("Logo_image", path.LogoPath));

        await emailService.SendMailWithTemplate(
            new MailMessageWithTemplate<MailForgotPasswordModel>(
                "Восстановление пароля",
                result.Data.Email, path.ForgotPasswordTemplatePath,
                new MailForgotPasswordModel(callbackUrl, inLine)),
            [],
            cancellationToken);

        return CreateResult.Success(new Message("Письмо отправлено"));
    }

    private async Task<Result<ForgotPasswordResult>> InnerHandler(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users
            .Where(u => u.Email == request.Email)
            .SingleOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<ForgotPasswordResult>(UserErrors.UserNotFound);
        }

        if (!user.EmailConfirmed)
        {
            auth.Rollback();
            return CreateResult.Failure<ForgotPasswordResult>(UserErrors.EmailNotConfirmed);
        }

        return CreateResult.Success(new ForgotPasswordResult(user.EmailConfirmed, user.Email, user.Id, user.Password));
    }
}
