using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.ConfirmedEmail;
public sealed class ConfirmedEmailHandler(
    ILogger<ConfirmedEmailHandler> logger,
    AuthContext auth) : IConfirmedEmailHandler
{
    public async Task<Result<Message>> Handle(ConfirmedEmailRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(ConfirmedEmailRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        if (!EmailConfirmed.IsEqualCode(request.Code, user.Email, user.Id.ToString()))
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.InvalidConfirmationCode);
        }

        user.EmailConfirmed = true;
        user.UpdateAt = DateTimeOffset.UtcNow;
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Электронная почта подтверждена"));
    }
}
