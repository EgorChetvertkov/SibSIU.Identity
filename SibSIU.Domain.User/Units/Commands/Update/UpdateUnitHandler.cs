using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Units.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Units.Commands.Update;
public sealed class UpdateUnitHandler(
    ILogger<UpdateUnitHandler> logger,
    AuthContext auth) : IUpdateUnitHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateUnitRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(CreateOrUpdateUnitRequest request, CancellationToken cancellationToken)
    {
        Unit? unit = await auth.Units
            .Where(u => u.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (unit is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UnitErrors.UnitNotFound);
        }

        bool alreadyExistsFullName = await auth.Units
            .Where(s => s.FullName.ToLower() == request.FullName.ToLower() && s.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (alreadyExistsFullName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UnitErrors.UnitFullNameAlreadyExists);
        }

        bool alreadyExistsShortName = await auth.Units
            .Where(s => s.ShortName.ToLower() == request.ShortName.ToLower() && s.Id != request.Id)
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

        unit.FullName = request.FullName;
        unit.Parent = parent;
        unit.ShortName = request.ShortName;
        unit.UpdateAt = DateTimeOffset.UtcNow;
        
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Подразделение успешно изменено"));
    }
}
