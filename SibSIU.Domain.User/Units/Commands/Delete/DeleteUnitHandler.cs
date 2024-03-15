using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Units.Commands.Delete;
public sealed class DeleteUnitHandler(
    ILogger<DeleteUnitHandler> logger,
    AuthContext auth) : IDeleteUnitHandler
{
    public async Task<Result<Message>> Handle(DeleteUnitRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request, async (request) =>
                await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandle(DeleteUnitRequest request, CancellationToken cancellationToken)
    {
        var counts = await auth.Units
            .Where(s => s.Id == request.Id)
            .Select(s => new {
                ChildrenCount = s.Children.Count,
                EmployeesCount = s.Employees.Count
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (counts is null || counts.ChildrenCount + counts.EmployeesCount > 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UnitErrors.UnitHasChildrenOrEmployee);
        }

        await auth.Units.Where(p => p.Id == request.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.IsActive, false)
                .SetProperty(p => p.UpdateAt, DateTimeOffset.UtcNow),
            cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Подразделение удалено"));
    }
}
