using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Scopes.Commands._Shared;

namespace SibSIU.Domain.UserManager.Scopes.Commands.Update;
public sealed class UpdateScopeHandler(
    ILogger<UpdateScopeHandler> logger,
    AuthContext auth) : IUpdateScopeHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateScopeRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(CreateOrUpdateScopeRequest request, CancellationToken cancellationToken)
    {
        Scope? scope = await auth.Scopes
            .Where(ct => ct.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (scope is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ScopeErrors.ScopeNotFound);
        }

        bool alreadyUseName = await auth.Scopes
            .Where(ct => ct.Name.ToLower() == request.Name.ToLower() && ct.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (alreadyUseName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ScopeErrors.ScopeNameAlreadyExists);
        }

        scope.Name = request.Name;
        scope.UpdateAt = DateTimeOffset.UtcNow;

        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Область успешно обновлена"));
    }
}
