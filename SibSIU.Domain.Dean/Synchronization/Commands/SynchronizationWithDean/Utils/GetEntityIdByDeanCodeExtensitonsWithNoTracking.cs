using Microsoft.EntityFrameworkCore;

using SibSIU.Auth.Database;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Utils;
internal static class GetEntityIdByDeanCodeExtensitonsWithNoTracking
{
    public static async Task<Ulid> GetInstituteId(this AuthContext context, int deanCode, CancellationToken cancellationToken)
    {
        return await context.InstituteUnits
            .AsNoTracking()
            .Where(i => i.Unit.DeanCode == deanCode)
            .Select(i => i.UnitId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public static async Task<Ulid> GetDepartmentId(this AuthContext context, int deanCode, CancellationToken cancellationToken)
    {
        return await context.DepartmentUnits
            .AsNoTracking()
            .Where(d => d.Unit.DeanCode == deanCode)
            .Select(d => d.UnitId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public static async Task<Ulid> GetDirectionOfTrainingId(this AuthContext context, int deanCode, CancellationToken cancellationToken)
    {
        return await context.DirectionOfTraining
            .AsNoTracking()
            .Where(dot => dot.DeanCode == deanCode)
            .Select(dot => dot.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public static async Task<Ulid> GetFormId(this AuthContext context, int deanCode, CancellationToken cancellationToken)
    {
        return await context.AcademicForms
            .AsNoTracking()
            .Where(f => f.DeanCode == deanCode)
            .Select(f => f.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public static async Task<Ulid> GetLevelId(this AuthContext context, int deanCode, CancellationToken cancellationToken)
    {
        return await context.AcademicLevels
            .AsNoTracking()
            .Where(l => l.DeanCode == deanCode)
            .Select(l => l.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public static async Task<Ulid> GetGroupId(this AuthContext context, string name, CancellationToken cancellationToken)
    {
        return await context.AcademicGroups
            .AsNoTracking()
            .Where(g => g.Name == name)
            .Select(g => g.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public static async Task<AcademicGroup?> GetGroup(this AuthContext context, string name, CancellationToken cancellationToken)
    {
        return await context.AcademicGroups
            .SingleOrDefaultAsync(g => g.Name == name, cancellationToken);
    }
}
