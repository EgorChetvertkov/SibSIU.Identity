using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Schools.Commands.Delete;
public sealed class DeleteSchoolHandler(
    ILogger<DeleteSchoolHandler> logger,
    AuthContext auth) : IDeleteSchoolHandler
{
    public async Task<Result<Message>> Handle(DeleteSchoolRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(DeleteSchoolRequest request, CancellationToken cancellationToken)
    {
        int countPupils = await auth.Schools
            .Where(s => s.Id == request.Id)
            .Select(s => s.Pupils.Count)
            .SingleOrDefaultAsync(cancellationToken);
        if (countPupils > 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolUse);
        }

        await auth.Schools.Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Школа удалена"));
    }
}
