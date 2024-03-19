using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Entities;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.AddClaim;
public sealed class AddClaimHandler(
    ILogger<AddClaimHandler> logger,
    AuthContext auth) : IAddClaimHandler
{
    public async Task<Result<Message>> Handle(AddClaimRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request,
                (request) => InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(AddClaimRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        AuthClaimType? claimType = await auth.ClaimTypes.GetById(request.ClaimTypeId, cancellationToken);
        if (claimType is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(ClaimTypeErrors.ClaimTypeNotFound);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        AuthClaim claim = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            ClaimType = claimType,
            User = user,
            Value = request.Value,
        };

        await auth.Claims.AddAsync(claim, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Утверждение создано"));
    }
}
