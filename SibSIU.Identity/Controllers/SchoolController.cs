using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Schools.Commands.Create;
using SibSIU.Domain.UserManager.Schools.Commands.Delete;
using SibSIU.Domain.UserManager.Schools.Commands.Update;
using SibSIU.Domain.UserManager.Schools.Queries.GetDetails;
using SibSIU.Domain.UserManager.Schools.Queries.GetPage;
using SibSIU.Domain.UserManager.Schools.Queries.GetSelectList;
using SibSIU.Identity.Models.Schools;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SchoolController(
    ICreateSchoolHandler create,
    IUpdateSchoolHandler update,
    IDeleteSchoolHandler delete,
    IGetSchoolDetailsHandler getDetails,
    IGetSchoolSelectListHandler getSelectList,
    IGetSchoolPageHandler getPage) : ControllerBase
{
    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(SchoolDetails school, CancellationToken cancellationToken)
    {
        var result = await create.Handle(new(school.Id, school.FullName, school.ShortName), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPut("update")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(SchoolDetails school, CancellationToken cancellationToken)
    {
        var result = await update.Handle(new(school.Id, school.FullName, school.ShortName), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpDelete("delete")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(Ulid postId, CancellationToken cancellationToken)
    {
        var result = await delete.Handle(new(postId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{schoolId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SchoolDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(Ulid schoolId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(schoolId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("list")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(List<SchoolItem>), StatusCodes.Status200OK)]
    public async Task<List<SchoolItem>> GetDetails(string filter, CancellationToken cancellationToken)
    {
        return await getSelectList.Handle(new(filter), cancellationToken);
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PaginationList<SchoolRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetSchoolPageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }
}
