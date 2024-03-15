using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Scopes.Commands._Shared;

namespace SibSIU.Domain.UserManager.Scopes.Commands.Create;
public sealed class CreateScopeHandler(
    ILogger<CreateScopeHandler> logger,
    AuthContext auth) : ICreateScopeHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateScopeRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(CreateOrUpdateScopeRequest request, CancellationToken cancellationToken)
    {
        bool alreadyUseName = await auth.Scopes
            .Where(ct => ct.Name.ToLower() == request.Name.ToLower())
            .AnyAsync(cancellationToken);
        if (alreadyUseName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ScopeErrors.ScopeNameAlreadyExists);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Scope scope = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Name = request.Name,
        };

        await auth.Scopes.AddAsync(scope, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Область успешно добавлена"));
    }
}
