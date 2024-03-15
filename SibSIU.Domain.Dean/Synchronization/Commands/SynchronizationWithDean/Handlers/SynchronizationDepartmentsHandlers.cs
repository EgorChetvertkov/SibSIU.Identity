using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Base;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;
internal static class SynchronizationDepartmentsHandlers
{
	public static async Task Synchronization(
		AuthContext identity,
		DeanContext dean,
		AcademicYear year,
		ILogger<SynchronizationWithDeanHandler> logger,
		CancellationToken cancellationToken)
	{
		List<SelectDeanDepartment> deanDepartments = await SelectSelectDeanDepartmentsList(dean, year.ToString(), cancellationToken);
		List<Unit> existsUnit = await SelectExiststDepartmentsList(identity, cancellationToken);
		List<SelectDeanDepartment> ownDeanDepartments = existsUnit
			.Select(u => new SelectDeanDepartment(u.DeanCode!.Value, u.Parent?.DeanCode ?? -1, u.FullName, u.ShortName))
			.ToList();

		logger.LogInformation($"По запросу синхронизации кафедр в базе данных Деканат обнаружено {deanDepartments.Count} записей, в базе данных системы обнаружено {ownDeanDepartments.Count} записей.");

		List<SelectDeanDepartment> forDelete = ownDeanDepartments.Except(deanDepartments).ToList();
		List<SelectDeanDepartment> forInsert = deanDepartments.Except(ownDeanDepartments).ToList();

		await Update(deanDepartments, existsUnit, identity, cancellationToken);

		await Delete(forDelete, identity, cancellationToken);

		await Insert(forInsert, identity, cancellationToken);
	}

	private static async Task Insert(List<SelectDeanDepartment> forInsert, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forInsert.Count > 0)
		{
			Dictionary<int, Ulid> deanCodeToId = [];

			List<Unit> inserting = [];
			foreach (var item in forInsert)
			{
				if (!deanCodeToId.TryGetValue(item.InstituteDeanCode, out Ulid parentId))
				{
					parentId = await identity.GetInstituteId(item.InstituteDeanCode, cancellationToken);
					deanCodeToId.Add(item.InstituteDeanCode, parentId);
				}

				if (parentId == default)
				{
					continue;
				}

				DateTime now = DateTime.UtcNow;
				inserting.Add(new()
				{
					Id = Ulid.NewUlid(now),
					DeanCode = item.DeanCode,
					IsActive = true,
					CreateAt = now,
					UpdateAt = now,
					FullName = item.FullName,
					ShortName = item.ShortName,
					ParentId = parentId,
					DepartmentUnit = new()
				});
			}

			await identity.Units.AddRangeAsync(inserting, cancellationToken);
			await identity.SaveChangesAsync(cancellationToken);
			identity.ChangeTracker.Clear();
		}
	}

	private static async Task Delete(List<SelectDeanDepartment> forDelete, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forDelete.Count > 0)
		{
			var deletingIds = forDelete.Select(u => u.DeanCode).ToList();

			DateTime now = DateTime.UtcNow;

			await identity.Units
				.Where(u => u.DeanCode != null && deletingIds.Contains(u.DeanCode.Value) && u.DepartmentUnit != null)
				.ExecuteUpdateAsync(u => u
					.SetProperty(u => u.IsActive, false)
					.SetProperty(u => u.UpdateAt, now),
					cancellationToken);
			await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Update(List<SelectDeanDepartment> deanInstitutes, List<Unit> existsUnit, AuthContext identity, CancellationToken cancellationToken)
	{
		Dictionary<int, Ulid> deanCodeToId = [];

		foreach (var own in existsUnit)
		{
			var dean = deanInstitutes.FirstOrDefault(i => i.DeanCode == own.DeanCode);

			if (dean is null)
			{
				continue;
			}

			if (!deanCodeToId.TryGetValue(dean.InstituteDeanCode, out Ulid parentId))
			{
				parentId = await identity.GetInstituteId(dean.InstituteDeanCode, cancellationToken);
				deanCodeToId.Add(dean.InstituteDeanCode, parentId);
			}

			if (parentId == default)
			{
				continue;
			}

			await identity.Units
				.Where(u => u.Id == own.Id)
				.ExecuteUpdateAsync(u => u
					.SetProperty(u => u.UpdateAt, DateTime.UtcNow)
					.SetProperty(u => u.ShortName, dean.ShortName)
					.SetProperty(u => u.FullName, dean.FullName)
					.SetProperty(u => u.ParentId, parentId)
					.SetProperty(u => u.IsActive, true),
					cancellationToken);
		}
    }

	private static async Task<List<Unit>> SelectExiststDepartmentsList(AuthContext identity, CancellationToken cancellationToken)
	{
		return await identity.Units
			.IgnoreQueryFilters()
			.Where(u => u.DepartmentUnit != null && u.DeanCode != null)
			.Include(u => u.DepartmentUnit)
			.Include(u => u.Parent)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	private static async Task<List<SelectDeanDepartment>> SelectSelectDeanDepartmentsList(DeanContext dean, string year, CancellationToken cancellationToken)
	{
		return await dean.ВсеГруппыs
			.Where(g => (g.УчебныйГод ?? year).CompareTo(year) >= 0 && g.Курс <= 6)
			.Join(dean.Специальностиs, g => g.КодСпециальности, s => s.Код,
			(g, s) => s.КодКафедры)
			.Join(dean.Кафедрыs.Where(d => d.КодФакультета != null), c => c, d => d.Код,
			(c, d) => new SelectDeanDepartment(d.Код, d.КодФакультета!.Value, d.Название!, d.Сокращение!))
			.Distinct()
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}
}
