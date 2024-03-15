using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Base;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;
internal static class SynchronizationInstitutesHandler
{
    public static async Task Synchronization(
        AuthContext identity,
        DeanContext dean,
        AcademicYear year,
        ILogger<SynchronizationWithDeanHandler> logger,
        CancellationToken cancellationToken)
    {
        List<SelectDeanInstitutes> deanInstitutes = await SelectSelectDeanInstituteList(dean, year.ToString(), cancellationToken);
        List<Unit> existsUnit = await SelectExistsInstituteList(identity, cancellationToken);
        List<SelectDeanInstitutes> ownDeanInstitutes = existsUnit
            .Select(u => new SelectDeanInstitutes(u.DeanCode!.Value, u.FullName, u.ShortName))
            .ToList();

        logger.LogInformation($"По запросу синхронизации институтов/факультетов в базе данных Деканат обнаружено {deanInstitutes.Count} записей, в базе данных системы обнаружено {ownDeanInstitutes.Count} записей.");

        List<SelectDeanInstitutes> forDelete = ownDeanInstitutes.Except(deanInstitutes).ToList();
        List<SelectDeanInstitutes> forInsert = deanInstitutes.Except(ownDeanInstitutes).ToList();

        await Update(deanInstitutes, existsUnit, identity, cancellationToken);

        await Delete(forDelete, identity, cancellationToken);

        await Insert(forInsert, identity, cancellationToken);
    }

    private static async Task Insert(List<SelectDeanInstitutes> forInsert, AuthContext identity, CancellationToken cancellationToken)
    {
        if (forInsert.Count > 0)
        {
            var seedId = await identity.Units
                .AsNoTracking()
                .Where(u => u.Parent == null)
                .Select(u => u.Id)
                .SingleOrDefaultAsync(cancellationToken);
            List<InstituteUnit> inserting = [];
            foreach (var item in forInsert)
            {
                DateTime now = DateTime.UtcNow;
                inserting.Add(new()
                {
                    Unit = new()
                    {
                        Id = Ulid.NewUlid(now),
                        DeanCode = item.DeanCode,
                        IsActive = true,
                        CreateAt = now,
                        UpdateAt = now,
                        FullName = item.FullName,
                        ShortName = item.ShortName,
                        ParentId = seedId
                    }
                });
            }

            await identity.InstituteUnits.AddRangeAsync(inserting, cancellationToken);
            await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
    }

    private static async Task Delete(List<SelectDeanInstitutes> forDelete, AuthContext identity, CancellationToken cancellationToken)
    {
        if (forDelete.Count > 0)
        {
            var deletingIds = forDelete.Select(u => u.DeanCode).ToList();

            DateTime now = DateTime.UtcNow;

            await identity.Units
                .Where(u => u.DeanCode != null && deletingIds.Contains(u.DeanCode.Value) && u.InstituteUnit != null)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.IsActive, false)
                    .SetProperty(u => u.UpdateAt, now),
                    cancellationToken);
            await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
    }

    private static async Task Update(List<SelectDeanInstitutes> deanInstitutes, List<Unit> existsUnit, AuthContext identity, CancellationToken cancellationToken)
    {
        List<Unit> updating = [];

        foreach (var own in existsUnit)
        {
            var dean = deanInstitutes.FirstOrDefault(i => i.DeanCode == own.DeanCode);

            if (dean is null)
            {
                continue;
            }

            own.UpdateAt = DateTime.UtcNow;
            own.ShortName = dean.ShortName;
            own.FullName = dean.FullName;
            own.IsActive = true;

            updating.Add(own);
        }

        identity.Units.UpdateRange(updating);
        await identity.SaveChangesAsync(cancellationToken);
        identity.ChangeTracker.Clear();
    }

    private static async Task<List<Unit>> SelectExistsInstituteList(AuthContext identity, CancellationToken cancellationToken)
    {
        return await identity.Units
            .IgnoreQueryFilters()
            .Where(u => u.InstituteUnit != null && u.DeanCode != null)
            .Include(u => u.InstituteUnit)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    private static async Task<List<SelectDeanInstitutes>> SelectSelectDeanInstituteList(DeanContext dean, string year, CancellationToken cancellationToken)
    {
        return await dean.ВсеГруппыs
                    .Where(g => (g.УчебныйГод ?? year).CompareTo(year) >= 0 && g.Курс <= 6)
                    .Join(dean.Специальностиs, g => g.КодСпециальности, s => s.Код,
                    (g, s) => s.КодКафедры)
                    .Join(dean.Кафедрыs, c => c, d => d.Код,
                    (c, d) => d.КодФакультета)
                    .Join(dean.Факультетыs, f => f, i => i.Код,
                    (f, i) => new SelectDeanInstitutes(i.Код, i.Факультет!, i.Сокращение!))
                    .Distinct()
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
	}
}
