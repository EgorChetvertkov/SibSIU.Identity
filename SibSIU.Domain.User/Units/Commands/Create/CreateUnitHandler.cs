using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Units.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Units.Commands.Create;
public sealed class CreateUnitHandler(
    ILogger<CreateUnitHandler> logger,
    AuthContext auth) : ICreateUnitHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateUnitRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(CreateOrUpdateUnitRequest request, CancellationToken cancellationToken)
    {
        bool alreadyExistsFullName = await auth.Units
            .Where(s => s.FullName.ToLower() == request.FullName.ToLower())
            .AnyAsync(cancellationToken);
        if (alreadyExistsFullName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UnitErrors.UnitFullNameAlreadyExists);
        }

        bool alreadyExistsShortName = await auth.Units
            .Where(s => s.ShortName.ToLower() == request.ShortName.ToLower())
            .AnyAsync(cancellationToken);
        if (alreadyExistsShortName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UnitErrors.UnitShortNameAlreadyExists);
        }

        Unit? parent = await auth.Units
            .Where(u => u.Id == request.ParentId)
            .SingleOrDefaultAsync(cancellationToken);
        if (parent is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UnitErrors.UnitParentNotFound);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Unit unit = new()
        {
            CreateAt = now,
            UpdateAt = now,
            Id = Ulid.NewUlid(now),
            IsActive = true,
            FullName = request.FullName,
            ShortName = request.ShortName,
            DeanCode = null,
            Parent = parent,
        };

        await auth.Units.AddAsync(unit, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Подразделение успешно создано"));
    }
}
