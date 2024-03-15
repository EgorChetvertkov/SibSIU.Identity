using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Schools.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Schools.Commands.Create;
public sealed class CreateSchoolHandler(
    ILogger<CreateSchoolHandler> logger,
    AuthContext auth) : ICreateSchoolHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateSchoolRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(CreateOrUpdateSchoolRequest request, CancellationToken cancellationToken)
    {
        bool alreadyExistsFullName = await auth.Schools
            .Where(s => s.FullName.ToLower() == request.FullName.ToLower())
            .AnyAsync(cancellationToken);
        if (alreadyExistsFullName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolFullNameAlreadyExists);
        }

        bool alreadyExistsShortName = await auth.Schools
            .Where(s => s.ShortName.ToLower() == request.ShortName.ToLower())
            .AnyAsync(cancellationToken);
        if (alreadyExistsShortName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolShortNameAlreadyExists);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        School school = new()
        {
            CreateAt = now,
            UpdateAt = now,
            Id = Ulid.NewUlid(now),
            IsActive = true,
            FullName = request.FullName,
            ShortName = request.ShortName,
        };

        await auth.Schools.AddAsync(school, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Школа успешно создана"));
    }
}
