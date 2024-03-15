using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.MailService.Mailer.Base;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.SendMail.MailModels;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePhone;
public sealed class ChangePhoneHandler(
    ILogger<ChangePhoneHandler> logger,
    MailPathForUserManager path,
    IEmailSender emailService,
    AuthContext auth) : IChangePhoneHandler
{
    public async Task<Result<Message>> Handle(ChangePhoneRequest request, CancellationToken cancellationToken)
    {
        var result = await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));

        if (result.IsFailure)
        {
            return CreateResult.Failure<Message>(result.Error);
        }

        if (result.Data.EmailConfirmed)
        {
            Dictionary<string, InLineAttachment> inLine = [];
            inLine.Add("logo", new("Logo_image", path.LogoPath));

            await emailService.SendMailWithTemplate(
                new MailMessageWithTemplate<MailPhoneChangeModel>("Изменение учетных данных", result.Data.Email, path.ChangePhoneTemplatePath, new MailPhoneChangeModel(request.PhoneNumber, inLine)),
                [],
                cancellationToken);
        }

        return CreateResult.Success(new Message("Номер телефона успешно изменен"));
    }

    private async Task<Result<ChangePhoneResult>> InnerHandler(ChangePhoneRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Пользователь {UserId} не найден", request.UserId);
            return CreateResult.Failure<ChangePhoneResult>(UserErrors.UserNotFound);
        }

        bool phoneAlreadyExists = await auth.Users.AnyUserHasPhone(request.PhoneNumber, cancellationToken);
        if (phoneAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Номер телефона {PhoneNumber} уже используется другим пользователем", request.PhoneNumber);
            return CreateResult.Failure<ChangePhoneResult>(UserErrors.PhoneAlreadyExists);
        }

        user.PhoneNumber = PhoneValidation.ReturnOnlyDigits(request.PhoneNumber);
        user.UpdateAt = DateTimeOffset.UtcNow;

        return CreateResult.Success(new ChangePhoneResult(user.EmailConfirmed, user.Email));
    }
}
