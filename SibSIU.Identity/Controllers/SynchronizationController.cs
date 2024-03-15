using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
using SibSIU.Identity.Models.AcademicGroups;
using SibSIU.Identity.Models.User.Imports;
using SibSIU.Identity.Models.User.Students;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SynchronizationController(
    ISynchronizationWithDeanHandler synchronization,
    IImportingStudentsHandler importingStudents,
    ISaveStudentsHandler saveStudents) : ControllerBase
{
    [HttpPost("update_data")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Synchronization(CancellationToken cancellationToken)
    {
        var result = await synchronization
            .Handle(new(), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("import_by_groups")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ComparativeUserBeforeImportList), StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportStudentGroups(List<string> groupNames, CancellationToken cancellationToken)
    {
        List<AcademicGroupItem> groupSelectItems = groupNames
            .Select(g => new AcademicGroupItem(g)).ToList();
        var result = await importingStudents
            .Handle(new(groupSelectItems), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("save")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CsvLoginData), StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveStudents(ComparativeUserBeforeImportList comparative, CancellationToken cancellationToken)
    {
        var result = await saveStudents
            .Handle(new(comparative), cancellationToken);
        return result.MapToActionResult();
    }
}
