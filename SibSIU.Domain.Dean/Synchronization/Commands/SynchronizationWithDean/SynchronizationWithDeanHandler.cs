using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Base;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Dean.Database;
using SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
public sealed class SynchronizationWithDeanHandler
    (ILogger<SynchronizationWithDeanHandler> logger,
    AuthContext identity,
    DeanContext dean)
    : ISynchronizationWithDeanHandler
{
    public async Task<Result<Message>> Handle(SynchronizationWithDeanRequest request, CancellationToken cancellationToken)
    {
        AcademicYear year = AcademicYear.GetCurrentAcademicYear();

        return await identity.WithTransaction(logger, request, async (request) =>
        {
            await SynchronizationInstitutesHandler.Synchronization(identity, dean, year, logger, cancellationToken);
            await SynchronizationDepartmentsHandlers.Synchronization(identity, dean, year, logger, cancellationToken);
            await SynchronizationFormsHandlers.Synchronization(identity, dean, year, logger, cancellationToken);
            await SynchronizationLevelsHandlers.Synchronization(identity, dean, year, logger, cancellationToken);
            await SynchronizationDirectionOfTrainingsHandlers.Synchronization(identity, dean, year, logger, cancellationToken);
            await SynchronizationGroupsHandlers.Synchronization(identity, dean, year, logger, cancellationToken);
            await SynchronizationStudentsHandlers.Synchronization(identity, dean, logger, cancellationToken);

            return CreateResult.Success(new Message("Данные успешно синхронизированы"));
        });
    }
}
