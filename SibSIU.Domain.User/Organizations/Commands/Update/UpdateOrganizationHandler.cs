using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Organizations.Commands._Shared;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Organizations.Commands.Update;
public sealed class UpdateOrganizationHandler(
    ILogger<UpdateOrganizationHandler> logger,
    AuthContext auth) : IUpdateOrganizationHandler
{
    public async Task<Result<Message>> Handle(CreateOrUpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(CreateOrUpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        Organization? organization = await auth.Organizations
            .Where(o => o.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (organization is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.OrganizationNotFound);
        }

        bool existsTIN = await auth.Organizations
            .Where(o => o.KPP == request.KPP && o.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (existsTIN)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.TINAlreadyExists);
        }

        bool existsOGRN = await auth.Organizations
            .Where(o => o.OGRN == request.OGRN && o.Id != request.Id)
            .AnyAsync(cancellationToken);
        if (existsOGRN)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.OGRNAlreadyExists);
        }

        organization.FullName = request.FullName;
        organization.ShortName = request.ShortName;
        organization.KPP = request.KPP;
        organization.TIN = request.TIN;
        organization.OGRN = request.OGRN;
        organization.UpdateAt = DateTimeOffset.UtcNow;

        return CreateResult.Success(new Message("Организация обновлена"));
    }
}
