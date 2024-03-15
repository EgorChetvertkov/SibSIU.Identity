using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;

namespace SibSIU.Domain.UserManager.Users.Commands.RemoveStudent;
public sealed class RemoveStudentHandler(
    ILogger<RemoveStudentHandler> logger,
    AuthContext auth) : IRemoveStudentHandler
{
    public async Task<Result<Message>> Handle(RemoveStudentRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(RemoveStudentRequest request, CancellationToken cancellationToken)
    {
        await auth.Students
            .Where(s => s.Id == request.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.IsActive, false)
                .SetProperty(s => s.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Студент откреплен от учетной записи"));
    }
}
