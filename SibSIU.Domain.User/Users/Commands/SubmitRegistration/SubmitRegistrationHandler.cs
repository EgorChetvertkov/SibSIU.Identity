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

namespace SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;
public sealed class SubmitRegistrationHandler(
    ILogger<SubmitRegistrationHandler> logger,
    MailPathForUserManager path,
    IEmailSender emailService,
    NavigationManager manager,
    AuthContext auth) : ISubmitRegistrationHandler
{
    public async Task<Result<Message>> Handle(SubmitRegistrationRequest request, CancellationToken cancellationToken)
    {
        Result<SubmitRegistrationResult> result = await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));

        if (result.IsFailure)
        {
            return CreateResult.Failure<Message>(result.Error);
        }


        Dictionary<string, InLineAttachment> inLine = [];
        inLine.Add("logo", new("Logo_image", path.LogoPath));
        await emailService.SendMailWithTemplate(
            new MailMessageWithTemplate<MailRegistrationModel>(
                "Регистрация на платформе СибГИУ",
                result.Data.Email,
                path.RegistrationTemplatePath,
                new MailRegistrationModel(
                    result.Data.Message,
                    request.Url.ToString(),
                    inLine)),
            [],
            cancellationToken);

        await EmailConfirmed.SendEmailConfirmationMail(emailService, path, manager, result.Data.Email, result.Data.UserId, cancellationToken);

        return CreateResult.Success(new Message("Заявка одобрена. Уведомления отправлены"));
    }

    private async Task<Result<SubmitRegistrationResult>> InnerHandle(SubmitRegistrationRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.IgnoreQueryFilters()
            .Where(u => u.IsActive && !u.IsConfirmedUser && u.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Пользователь {UserId} не обнаружен", request.Id);
            return CreateResult.Failure<SubmitRegistrationResult>(UserErrors.UserNotFound);
        }

        user.IsConfirmedUser = true;

        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new SubmitRegistrationResult(user.EmailConfirmed, user.Email, "Ваша заявка одобрена. Вы можете пользоваться учетной записью СибГИУ", user.Id.ToString()));
    }
}
