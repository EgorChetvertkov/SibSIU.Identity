﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.ClaimTypes.Commands._Shared;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands.Update;
public sealed class UpdateClaimTypeHandler(
    ILogger<UpdateClaimTypeHandler> logger,
    AuthContext auth) : IUpdateClaimTypeHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateClaimTypeRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(CreateOrUpdateClaimTypeRequest request, CancellationToken cancellationToken)
    {
        AuthClaimType? claimType = await auth.ClaimTypes
            .Where(ct => ct.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (claimType is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ClaimTypeErrors.ClaimTypeNotFound);
        }

        bool alreadyUseName = await auth.ClaimTypes
            .Where(ct => ct.Name.ToLower() == request.Name.ToLower() && ct.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (alreadyUseName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ClaimTypeErrors.ClaimTypeNameAlreadyExists);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;

        claimType.Name = request.Name;
        claimType.IncludeInAccessToken = request.IncludeInAccessToken;
        claimType.IncludeInIdentityToken = request.IncludeInIdentityToken;
        claimType.UpdateAt = now;

        await auth.ClaimTypeSettings
            .Where(c => c.ClaimTypeId == claimType.Id)
            .ExecuteDeleteAsync(cancellationToken);

        List<Scope> scopes = await auth.Scopes
            .Where(s => request.Scopes.Contains(s.Id))
            .ToListAsync(cancellationToken);
        List<AuthClaimTypeScopes> authClaimTypeScopes = [];
        foreach (var scope in scopes)
        {
            authClaimTypeScopes.Add(new()
            {
                Id = Ulid.NewUlid(now),
                CreateAt = now,
                UpdateAt = now,
                IsActive = true,
                ClaimTypeId = claimType.Id,
                ScopeId = scope.Id
            });
        }

        await auth.ClaimTypeSettings.AddRangeAsync(authClaimTypeScopes, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Тип утверждения успешно обновлен"));
    }
}
