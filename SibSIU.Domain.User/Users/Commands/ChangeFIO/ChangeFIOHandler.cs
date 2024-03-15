using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeFIO;
public sealed class ChangeFIOHandler(
    ILogger<ChangeFIOHandler> logger,
    AuthContext auth) : IChangeFIOHandler
{
    public async Task<Result<Message>> Handle(ChangeFIORequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(ChangeFIORequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            logger.LogError("Не удалось обнаружить пользователя {UserId}", request.UserId);
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Patronymic = request.Patronymic;
        
        return CreateResult.Success(new Message("Фамилия, имя и отчество успешно изменены"));
    }
}
