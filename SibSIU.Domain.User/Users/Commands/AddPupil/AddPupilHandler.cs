using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.Extensions;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Commands.AddPupil;
public sealed class AddPupilHandler(
    ILogger<AddPupilHandler> logger,
    AuthContext auth) : IAddPupilHandler
{
    public async Task<Result<Message>> Handle(AddPupilRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    public async Task<Result<Message>> InnerHandle(AddPupilRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        int countPupils = await auth.Pupils
            .Where(p => p.UserId == request.UserId)
            .CountAsync(cancellationToken);
        if (countPupils != 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.CountActivePupilsMustBeZeroForAddNew);
        }

        School? school = await auth.Schools.GetById(request.SchoolId, cancellationToken);
        if (school is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(SchoolErrors.SchoolNotFound);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Pupil pupil = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            ClassLitter = request.ClassLitter,
            ClassNumber = request.ClassNumber,
            School = school,
            User = user,
        };

        await auth.Pupils.AddAsync(pupil, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Пользователю добавлены данные школьника"));
    }
}
