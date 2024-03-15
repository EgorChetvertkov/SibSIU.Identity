using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.MailService.Mailer.Base;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.SendMail.MailModels;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Web.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;
public sealed class ChangeUserNameHandler(
    ILogger<ChangeUserNameHandler> logger,
    MailPathForUserManager path,
    IEmailSender emailService,
    AuthContext auth) : IChangeUserNameHandler
{
    public async Task<Result<Message>> Handle(ChangeUserNameRequest request, CancellationToken cancellationToken)
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
                new MailMessageWithTemplate<MailUserNameChangeModel>("Изменение учетных данных", result.Data.Email, path.ChangeUserNameTemplatePath, new MailUserNameChangeModel(request.UserName, inLine)),
                [],
                cancellationToken);
        }

        return CreateResult.Success(new Message("Имя пользователя успешно изменено"));
    }

    private async Task<Result<ChangeUserNameResult>> InnerHandler(ChangeUserNameRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Пользователь {UserId} не найден", request.UserId);
            return CreateResult.Failure<ChangeUserNameResult>(UserErrors.UserNotFound);
        }

        bool userNameAlreadyExists = await auth.Users
            .Where(u => u.UserName == request.UserName)
            .AnyAsync(cancellationToken);
        if (userNameAlreadyExists)
        {
            auth.Rollback();
            logger.LogError("Имя пользователя {UserName} используется другим пользователем", request.UserName);
            return CreateResult.Failure<ChangeUserNameResult>(UserErrors.UserNameAlreadyExists);
        }

        user.UserName = request.UserName;
        user.UpdateAt = DateTimeOffset.UtcNow.ToUniversalTime();

        return CreateResult.Success<ChangeUserNameResult>(new(user.EmailConfirmed, user.Email));
    }
}
