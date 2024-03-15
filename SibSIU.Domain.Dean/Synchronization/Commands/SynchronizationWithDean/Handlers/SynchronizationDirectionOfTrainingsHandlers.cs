using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Base;
using SibSIU.Dean.Database;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Utils;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Identity.Dean.Commands.SynchronizationWithDean.SynchronizationHandlers;
internal static class SynchronizationDirectionOfTrainingsHandlers
{
	public static async Task Synchronization(
		AuthContext identity,
		DeanContext dean,
		AcademicYear year,
		ILogger<SynchronizationWithDeanHandler> logger,
		CancellationToken cancellationToken)
	{
		List<SelectDirectionOfTraining> deanDOTs = await SelectSelectDotList(dean, year.ToString(), cancellationToken);
		List<DirectionOfTraining> existsDOTs = await SelectExists(identity, cancellationToken);

		List<SelectDirectionOfTraining> ownDeanDots = existsDOTs
			.Select(u => new SelectDirectionOfTraining(u.DeanCode, u.Name, u.Code, u.ImplementingChair.Unit.DeanCode ?? -1, u.DeveloperInstitute.Unit.DeanCode ?? -1))
			.ToList();

		List<SelectDirectionOfTraining> forDelete = ownDeanDots.Except(deanDOTs).ToList();
		List<SelectDirectionOfTraining> forInsert = deanDOTs.Except(ownDeanDots).ToList();

		await Update(deanDOTs, existsDOTs, identity, cancellationToken);

		await Delete(forDelete, identity, cancellationToken);

		await Insert(forInsert, identity, cancellationToken);
	}

	private static async Task Insert(List<SelectDirectionOfTraining> forInsert, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forInsert.Count > 0)
		{
			List<DirectionOfTraining> inserting = [];
			Dictionary<int, Ulid> departmentsIds = [];
			Dictionary<int, Ulid> instituteIds = [];
			foreach (var item in forInsert)
			{
				if (!departmentsIds.TryGetValue(item.DepartmentDeanCode, out Ulid departmentId))
				{
					departmentId = await identity.GetDepartmentId(item.DepartmentDeanCode, cancellationToken);
					departmentsIds.Add(item.DepartmentDeanCode, departmentId);
				}

				if (!instituteIds.TryGetValue(item.InstituteDeanCode, out Ulid instituteId))
				{
					instituteId = await identity.GetInstituteId(item.InstituteDeanCode, cancellationToken);
					instituteIds.Add(item.InstituteDeanCode, instituteId);
				}
				
				if (departmentId == default || instituteId == default)
				{
					continue;
				}

				DateTime now = DateTime.UtcNow;
				inserting.Add(new()
				{
					Id = Ulid.NewUlid(now),
					IsActive = true,
					CreateAt = now,
					UpdateAt = now,
					Code = item.Code,
					Name = item.Name,
					DeanCode = item.DeanCode,
					DeveloperInstituteId = instituteId,
					ImplementingChairId = departmentId,
				});
			}

			await identity.DirectionOfTraining.AddRangeAsync(inserting, cancellationToken);
			_ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Delete(List<SelectDirectionOfTraining> forDelete, AuthContext identity, CancellationToken cancellationToken)
	{
		if (forDelete.Count > 0)
		{
			var deletingIds = forDelete.Select(u => u.DeanCode).ToList();

			DateTime now = DateTime.UtcNow;

			await identity.DirectionOfTraining
				.Where(u => deletingIds.Contains(u.DeanCode))
				.ExecuteUpdateAsync(u => u
					.SetProperty(u => u.IsActive, false)
					.SetProperty(u => u.UpdateAt, now),
					cancellationToken);
			_ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Update(List<SelectDirectionOfTraining> deanDOTs, List<DirectionOfTraining> existsDOTs, AuthContext identity, CancellationToken cancellationToken)
	{
		List<DirectionOfTraining> updating = [];
		Dictionary<int, Ulid> departmentsIds = [];
		Dictionary<int, Ulid> instituteIds = [];

		foreach (var own in existsDOTs)
		{
			var dean = deanDOTs.FirstOrDefault(i => i.Equals(own));
			if (dean is null)
			{
				continue;
			}

			if (!departmentsIds.TryGetValue(dean.DepartmentDeanCode, out Ulid departmentId))
			{
				departmentId = await identity.GetDepartmentId(dean.DepartmentDeanCode, cancellationToken);
				departmentsIds.Add(dean.DepartmentDeanCode, departmentId);
			}

			if (!instituteIds.TryGetValue(dean.InstituteDeanCode, out Ulid instituteId))
			{
				instituteId = await identity.GetInstituteId(dean.InstituteDeanCode, cancellationToken);
				instituteIds.Add(dean.InstituteDeanCode, instituteId);
			}

			if (departmentId == default || instituteId == default)
			{
				continue;
			}

			own.UpdateAt = DateTime.UtcNow;
			own.Code = dean.Code;
			own.Name = dean.Name;
			own.DeveloperInstituteId = instituteId;
			own.ImplementingChairId = departmentId;
			own.IsActive = true;
			updating.Add(own);
		}

		identity.DirectionOfTraining.UpdateRange(updating);
		_ = await identity.SaveChangesAsync(cancellationToken);
        identity.ChangeTracker.Clear();
    }

	private static async Task<List<DirectionOfTraining>> SelectExists(AuthContext identity, CancellationToken cancellationToken)
	{
		return await identity.DirectionOfTraining
				.IgnoreQueryFilters()
				.Include(d => d.DeveloperInstitute)
					.ThenInclude(i => i.Unit)
				.Include(d => d.ImplementingChair)
					.ThenInclude(c => c.Unit)
				.AsNoTracking()
				.ToListAsync(cancellationToken);
	}

	private static async Task<List<SelectDirectionOfTraining>> SelectSelectDotList(DeanContext dean, string year, CancellationToken cancellationToken)
	{
		return await dean.ВсеГруппыs
			.Where(g =>
				(g.УчебныйГод ?? year).CompareTo(year) >= 0 && g.Курс <= 6)
			.GroupBy(g => g.КодСпециальности)
			.Select(g => g.Key)
			.Join(dean.Специальностиs, g => g, s => s.Код,
			(g, s) => new SelectDirectionOfTraining(s.Код, s.НазваниеСпец, s.Специальность, s.КодКафедры, s.КодФакультета))
			.Distinct()
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}
}
