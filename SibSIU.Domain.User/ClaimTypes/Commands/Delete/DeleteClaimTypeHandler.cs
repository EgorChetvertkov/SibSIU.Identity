﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands.Delete;
public sealed class DeleteClaimTypeHandler(
    ILogger<DeleteClaimTypeHandler> logger,
    AuthContext auth) : IDeleteClaimTypeHandler
{
    public async Task<Result<Message>> Handle(DeleteClaimTypeRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(DeleteClaimTypeRequest request, CancellationToken cancellationToken)
    {
        int countClaims = await auth.ClaimTypes
            .Where(s => s.Id == request.Id)
            .Select(s => s.Claims.Count)
            .SingleOrDefaultAsync(cancellationToken);
        if (countClaims > 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ClaimTypeErrors.ClaimTypeUse);
        }

        await auth.ClaimTypes.Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Тип утверждения удален"));
    }
}
