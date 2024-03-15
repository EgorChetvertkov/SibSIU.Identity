using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Delete;
public sealed class DeleteOrganizationHandler(
    ILogger<DeleteOrganizationHandler> logger,
    AuthContext auth) : IDeleteOrganizationHandler
{
    public async Task<Result<Message>> Handle(DeleteOrganizationRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(DeleteOrganizationRequest request, CancellationToken cancellationToken)
    {
        var countWorker = await auth.Organizations
            .Where(o => o.Id == request.Id)
            .Select(o => o.Partners.Count)
            .SingleOrDefaultAsync(cancellationToken);
        if (countWorker > 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.OrganizationUse);
        }

        await auth.Organizations
            .Where(o => o.Id == request.Id)
            .ExecuteUpdateAsync(o => o
                .SetProperty(o => o.IsActive, false)
                .SetProperty(o => o.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Организация удалена"));
    }
}
