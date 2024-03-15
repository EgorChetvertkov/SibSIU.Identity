using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Organizations.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Create;
public sealed class CreateOrganizationHandler(
    ILogger<CreateOrganizationHandler> logger,
    AuthContext auth) : ICreateOrganizationHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(CreateOrUpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        bool existsTIN = await auth.Organizations
            .Where(o => o.KPP == request.KPP)
            .AnyAsync(cancellationToken);
        if (existsTIN)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.TINAlreadyExists);
        }

        bool existsOGRN = await auth.Organizations
            .Where(o => o.OGRN == request.OGRN)
            .AnyAsync(cancellationToken);
        if (existsOGRN)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.OGRNAlreadyExists);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Organization organization = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            FullName = request.FullName,
            ShortName = request.ShortName,
            KPP = request.KPP,
            OGRN = request.OGRN,
            TIN = request.TIN,
        };

        await auth.Organizations.AddAsync(organization, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Организация добавлена"));
    }
}
