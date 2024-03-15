using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeBirthday;
public sealed class ChangeBirthdayHandler(
    ILogger<ChangeBirthdayHandler> logger,
    AuthContext auth) : IChangeBirthdayHandler
{
    public async Task<Result<Message>> Handle(ChangeBirthdayRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(ChangeBirthdayRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось обнаружить пользователя {UserId}", request.UserId);
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        user.BirthOfDate = request.Birthday.Date;
        user.UpdateAt = DateTimeOffset.UtcNow;

        return CreateResult.Success(new Message("Дата рождения успешно изменена"));
    }
}
