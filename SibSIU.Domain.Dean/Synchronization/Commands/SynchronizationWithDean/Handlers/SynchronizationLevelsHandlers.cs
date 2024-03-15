using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Base;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;
internal static class SynchronizationLevelsHandlers
{
	public static async Task Synchronization(
		AuthContext identity,
		DeanContext dean,
		AcademicYear year,
		ILogger<SynchronizationWithDeanHandler> logger,
		CancellationToken cancellationToken)
	{
		List<SelectDeanLevel> deanLevels = await SelectSelectEducationLevelsList(dean, year.ToString(), cancellationToken);
		List<AcademicLevel> existsLevels = await SelectExists(identity, cancellationToken);

		List<SelectDeanLevel> ownDeanForm = existsLevels
			.Select(u => new SelectDeanLevel(u.DeanCode, u.FullName, u.ShortName))
			.ToList();

		List<SelectDeanLevel> forDelete = ownDeanForm.Except(deanLevels).ToList();
		List<SelectDeanLevel> forInsert = deanLevels.Except(ownDeanForm).ToList();

		await Update(deanLevels, existsLevels, identity, cancellationToken);

		await Delete(forDelete, identity, cancellationToken);

		await Insert(forInsert, identity, cancellationToken);
	}

	private static async Task Insert(List<SelectDeanLevel> forInsert, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forInsert.Count > 0)
		{
			List<AcademicLevel> inserting = new();
			foreach (var item in forInsert)
			{
				DateTime now = DateTime.UtcNow;
				inserting.Add(new()
				{
					Id = Ulid.NewUlid(),
					DeanCode = item.DeanCode,
					IsActive = true,
					CreateAt = now,
					UpdateAt = now,
					FullName = item.FullName,
					ShortName = item.ShortName,
				});
			}

			await identity.AcademicLevels.AddRangeAsync(inserting, cancellationToken);
			_ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Delete(List<SelectDeanLevel> forDelete, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forDelete.Count > 0)
		{
			var deletingIds = forDelete.Select(u => u.DeanCode).ToList();

			DateTime now = DateTime.UtcNow;

			await identity.AcademicLevels
				.Where(u => deletingIds.Contains(u.DeanCode))
				.ExecuteUpdateAsync(u => u
					.SetProperty(u => u.IsActive, false)
					.SetProperty(u => u.UpdateAt, now),
					cancellationToken);
			_ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Update(List<SelectDeanLevel> deanLevels, List<AcademicLevel> existsLevels, AuthContext identity, CancellationToken cancellationToken)
	{
		List<AcademicLevel> updating = [];
		foreach (var own in existsLevels)
		{
			var dean = deanLevels.FirstOrDefault(i => i.DeanCode == own.DeanCode);

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

		identity.AcademicLevels.UpdateRange(updating);
		_ = await identity.SaveChangesAsync(cancellationToken);
        identity.ChangeTracker.Clear();
    }

	private static async Task<List<AcademicLevel>> SelectExists(AuthContext identity, CancellationToken cancellationToken)
	{
		return await identity.AcademicLevels
			.IgnoreQueryFilters()
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	private static async Task<List<SelectDeanLevel>> SelectSelectEducationLevelsList(DeanContext dean, string year, CancellationToken cancellationToken)
	{
		return await dean.ВсеГруппыs
			.Where(g => (g.УчебныйГод ?? year).CompareTo(year) >= 0 && g.Курс <= 6)
			.GroupBy(g => g.ФормаОбучения)
			.Select(g => g.Key)
			.Join(dean.УровеньОбразованияs, g => g, f => f.КодЗаписи,
			(g, f) => new SelectDeanLevel(f.КодЗаписи, f.Уровень!, f.Категория!))
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}
}
