using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.MailService.Mailer.Base;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.SendMail.MailModels;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.RejectRegistration;
public sealed class RejectRegistrationHandler(
    ILogger<SubmitRegistrationHandler> logger,
    MailPathForUserManager path,
    IEmailSender emailService,
    AuthContext auth) : IRejectRegistrationHandler
{
    public async Task<Result<Message>> Handle(RejectRegistrationRequest request, CancellationToken cancellationToken)
    {
        Result<RejectRegistrationResult> result = await request.Ensure(async (request) =>
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
        }

        return CreateResult.Success(new Message("Заявка отклонена. Уведомления отправлены"));
    }

    private async Task<Result<RejectRegistrationResult>> InnerHandle(RejectRegistrationRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.IgnoreQueryFilters()
            .Where(u => u.IsActive && !u.IsConfirmedUser && u.Id == request.UserId)
            .SingleOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Пользователь {UserId} не обнаружен", request.UserId);
            return CreateResult.Failure<RejectRegistrationResult>(UserErrors.UserNotFound);
        }

        auth.Users.Remove(user);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new RejectRegistrationResult(user.EmailConfirmed, user.Email, $"Ваша заявка отклонена по следующей причине: {request.Message}"));
    }
}
