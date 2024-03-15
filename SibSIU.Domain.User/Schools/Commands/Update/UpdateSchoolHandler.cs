using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Schools.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Schools.Commands.Update;
public sealed class UpdateSchoolHandler(
    ILogger<UpdateSchoolHandler> logger,
    AuthContext auth) : IUpdateSchoolHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateSchoolRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(CreateOrUpdateSchoolRequest request, CancellationToken cancellationToken)
    {
        School? school = await auth.Schools
            .Where(s => s.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (school is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolNotFound);
        }

        bool alreadyExistsFullName = await auth.Schools
            .Where(s => s.FullName.ToLower() == request.FullName.ToLower() && s.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (alreadyExistsFullName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolFullNameAlreadyExists);
        }

        bool alreadyExistsShortName = await auth.Schools
            .Where(s => s.ShortName.ToLower() == request.ShortName.ToLower() && s.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (alreadyExistsShortName)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolShortNameAlreadyExists);
        }

        school.ShortName = request.ShortName;
        school.FullName = request.FullName;
        school.UpdateAt = DateTimeOffset.UtcNow;

        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Школа успешно изменена"));
    }
}
