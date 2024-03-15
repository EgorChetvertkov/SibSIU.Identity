using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Base;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;
internal static class SynchronizationFormsHandlers
{
	public static async Task Synchronization(
		AuthContext identity,
		DeanContext dean,
		AcademicYear year,
		ILogger<SynchronizationWithDeanHandler> logger,
		CancellationToken cancellationToken)
	{
		List<SelectDeanForm> deanForms = await SelectSelectDeanFormList(dean, year.ToString(), cancellationToken);
		List<AcademicForm> existsForms = await SelectExists(identity, cancellationToken);

		List<SelectDeanForm> ownDeanForms = existsForms
			.Select(u => new SelectDeanForm(u.DeanCode, u.FullName, u.ShortName))
			.ToList();

		logger.LogInformation($"По запросу синхронизации форм обучения в базе данных Деканат обнаружено {deanForms.Count} записей, в базе данных системы обнаружено {ownDeanForms.Count} записей.");

		List<SelectDeanForm> forDelete = ownDeanForms.Except(deanForms).ToList();
		List<SelectDeanForm> forInsert = deanForms.Except(ownDeanForms).ToList();

		await Update(deanForms, existsForms, identity, cancellationToken);

		await Delete(forDelete, identity, cancellationToken);

		await Insert(forInsert, identity, cancellationToken);
	}

	private static async Task Insert(List<SelectDeanForm> forInsert, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forInsert.Count > 0)
		{
			List<AcademicForm> inserting = [];
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

			await identity.AcademicForms.AddRangeAsync(inserting, cancellationToken);
			_ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Delete(List<SelectDeanForm> forDelete, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forDelete.Count > 0)
		{
			var deletingIds = forDelete.Select(u => u.DeanCode).ToList();

			DateTime now = DateTime.UtcNow;

			await identity.AcademicForms
				.Where(u => deletingIds.Contains(u.DeanCode))
				.ExecuteUpdateAsync(u => u
					.SetProperty(u => u.IsActive, false)
					.SetProperty(u => u.UpdateAt, now),
					cancellationToken);
			_ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Update(List<SelectDeanForm> deanForms, List<AcademicForm> existsForms, AuthContext identity, CancellationToken cancellationToken)
	{
		List<AcademicForm> updating = [];
		foreach (var own in existsForms)
		{
			var dean = deanForms.FirstOrDefault(i => i.DeanCode == own.DeanCode);
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

		identity.AcademicForms.UpdateRange(updating);
		_ = await identity.SaveChangesAsync(cancellationToken);
        identity.ChangeTracker.Clear();
    }

	private static async Task<List<AcademicForm>> SelectExists(AuthContext identity, CancellationToken cancellationToken)
	{
		return await identity.AcademicForms
				.IgnoreQueryFilters()
				.AsNoTracking()
				.ToListAsync(cancellationToken);
	}

	private static async Task<List<SelectDeanForm>> SelectSelectDeanFormList(DeanContext dean, string year, CancellationToken cancellationToken)
	{
		return await dean.ВсеГруппыs
			.Where(g => (g.УчебныйГод ?? year).CompareTo(year) >= 0 && g.Курс <= 6)
			.GroupBy(g => g.ФормаОбучения)
			.Select(g => g.Key)
			.Join(dean.ФормаОбученияs, g => g, f => f.Код,
			(g, f) => new SelectDeanForm(f.Код, f.ФормаОбучения1!, f.Сокращение!))
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}
}
