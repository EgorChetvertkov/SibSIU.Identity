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
internal static class SynchronizationGroupsHandlers
{
	public static async Task Synchronization(
		AuthContext identity,
		DeanContext dean,
        AcademicYear year,
		ILogger<SynchronizationWithDeanHandler> logger,
		CancellationToken cancellationToken)
	{
		List<SelectDeanGroup> deanGroup = await SelectSelectGroupList(dean, year.ToString(), cancellationToken);
		List<AcademicGroup> existsGroup = await SelectExists(identity, cancellationToken);

		List<SelectDeanGroup> ownDeanGroup = existsGroup
			.Select(g => new SelectDeanGroup(g.Name, year.EndYear - g.StartYear, g.Level.DeanCode, g.Form.DeanCode, g.DirectionOfTraining.DeanCode, g.DirectorateInstitute.Unit.DeanCode ?? -1))
			.ToList();

		List<SelectDeanGroup> forDelete = ownDeanGroup.Except(deanGroup).ToList();
		List<SelectDeanGroup> forInsert = deanGroup.Except(ownDeanGroup).ToList();

		await Update(deanGroup, existsGroup, year, identity, cancellationToken);

		await Delete(forDelete, identity, cancellationToken);

		await Insert(year, forInsert, identity, cancellationToken);
	}

	private static async Task Insert(AcademicYear year, List<SelectDeanGroup> forInsert, AuthContext identity, CancellationToken cancellationToken)
    {
        if (forInsert.Count > 0)
        {
            List<AcademicGroup> inserting = [];
			Dictionary<int, Ulid> dots = [];
			Dictionary<int, Ulid> levels = [];
			Dictionary<int, Ulid> forms = [];
			Dictionary<int, Ulid> institutes = [];
            foreach (var dean in forInsert)
            {
				if(!dots.TryGetValue(dean.DirectionOfTrainingDeanCode, out Ulid dotId))
				{
                    dotId = await identity.GetDirectionOfTrainingId(dean.DirectionOfTrainingDeanCode, cancellationToken);
					dots.Add(dean.DirectionOfTrainingDeanCode, dotId);
				}

				if(!forms.TryGetValue(dean.AcademicFormDeanCode, out Ulid formId))
				{
					formId = await identity.GetFormId(dean.AcademicFormDeanCode, cancellationToken);
					forms.Add(dean.AcademicFormDeanCode, formId);
				}

				if(!levels.TryGetValue(dean.AcademicLevelDeanCode, out Ulid levelId))
				{
                    levelId = await identity.GetLevelId(dean.AcademicLevelDeanCode, cancellationToken);
					levels.Add(dean.AcademicLevelDeanCode, levelId);
				}

				if (!institutes.TryGetValue(dean.InstituteDeanCode, out Ulid instituteId))
				{
                    instituteId = await identity.GetInstituteId(dean.InstituteDeanCode, cancellationToken);
					institutes.Add(dean.InstituteDeanCode, instituteId);
				}

                if (dotId == default ||
					formId == default ||
					levelId == default ||
					instituteId == default)
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
					DirectionOfTrainingId = dotId,
					AcademicFormId = formId,
					DirectorateInstituteId = instituteId,
					AcademicLevelId = levelId,
                    Name = dean.Name,
                    StartYear = year.EndYear - dean.Course,
                });
            }

            await identity.AcademicGroups.AddRangeAsync(inserting, cancellationToken);
            _ = await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
    }

	private static async Task Delete(List<SelectDeanGroup> forDelete, AuthContext identity, CancellationToken cancellationToken)
	{
        if (forDelete.Count > 0)
        {
            var deleteIds = forDelete.Select(g => g.Name).ToList();

            DateTime now = DateTime.UtcNow;

            await identity.AcademicGroups.Where(g => deleteIds.Contains(g.Name))
                .ExecuteUpdateAsync(g => g
                    .SetProperty(g => g.IsActive, false)
                    .SetProperty(g => g.UpdateAt, now),
                    cancellationToken);
            await identity.SaveChangesAsync(cancellationToken);
            identity.ChangeTracker.Clear();
        }
	}

	private static async Task Update(List<SelectDeanGroup> deanGroup, List<AcademicGroup> existsGroup, AcademicYear year, AuthContext identity, CancellationToken cancellationToken)
    {
        List<AcademicGroup> updating = [];
        Dictionary<int, Ulid> dots = [];
        Dictionary<int, Ulid> levels = [];
        Dictionary<int, Ulid> forms = [];
        Dictionary<int, Ulid> institutes = [];

        foreach (var own in existsGroup)
        {
            var dean = deanGroup.FirstOrDefault(i => i.Equals(own));
            if (dean is null)
            {
                continue;
            }

            if (!dots.TryGetValue(dean.DirectionOfTrainingDeanCode, out Ulid dotId))
			{
				dotId = await identity.GetDirectionOfTrainingId(dean.DirectionOfTrainingDeanCode, cancellationToken);
				dots.Add(dean.DirectionOfTrainingDeanCode, dotId);
            }

            if (!forms.TryGetValue(dean.AcademicFormDeanCode, out Ulid formId))
            {
                formId = await identity.GetFormId(dean.AcademicFormDeanCode, cancellationToken);
                forms.Add(dean.AcademicFormDeanCode, formId);
            }

            if (!levels.TryGetValue(dean.AcademicLevelDeanCode, out Ulid levelId))
			{
				levelId = await identity.GetLevelId(dean.AcademicLevelDeanCode, cancellationToken);
				levels.Add(dean.AcademicLevelDeanCode, levelId);
            }

            if (!institutes.TryGetValue(dean.InstituteDeanCode, out Ulid instituteId))
			{
				instituteId = await identity.GetInstituteId(dean.InstituteDeanCode, cancellationToken);
				institutes.Add(dean.InstituteDeanCode, instituteId);
            }

            if (dotId == default ||
                formId == default ||
                levelId == default ||
                instituteId == default)
            {
                continue;
            }

            own.UpdateAt = DateTime.UtcNow;
            own.DirectionOfTrainingId = dotId;
            own.AcademicFormId = formId;
			own.AcademicLevelId = levelId;
            own.DirectorateInstituteId = instituteId;
            own.Name = dean.Name;
            own.StartYear = year.EndYear - dean.Course;
            own.IsActive = true;
            updating.Add(own);
        }

        identity.AcademicGroups.UpdateRange(updating);
        _ = await identity.SaveChangesAsync(cancellationToken);
        identity.ChangeTracker.Clear();
    }

	private static async Task<List<AcademicGroup>> SelectExists(AuthContext identity, CancellationToken cancellationToken)
    {
        return await identity.AcademicGroups
			.IgnoreQueryFilters()
			.Include(g => g.DirectionOfTraining)
			.Include(g => g.Level)
			.Include(g => g.Form)
			.Include(g => g.DirectorateInstitute)
				.ThenInclude(dot => dot.Unit)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
    }

	private static async Task<List<SelectDeanGroup>> SelectSelectGroupList(DeanContext dean, string year, CancellationToken cancellationToken)
    {
        return await dean.ВсеГруппыs
            .Where(g =>
                (g.УчебныйГод ?? year).CompareTo(year) >= 0 && g.Курс <= 6)
            .GroupBy(g => new { g.Название, g.ФормаОбучения, g.КодСпециальности, g.Уровень, g.КодФакультета })
            .Select(g => new SelectDeanGroup(
				g.Key.Название,
				g.Max(g => g.Курс)!.Value,
                g.Key.Уровень!.Value,
				g.Key.ФормаОбучения!.Value,
				g.Key.КодСпециальности!.Value,
				g.Key.КодФакультета!.Value))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
