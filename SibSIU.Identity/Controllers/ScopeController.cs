using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Scopes.Commands.Create;
using SibSIU.Domain.UserManager.Scopes.Commands.Delete;
using SibSIU.Domain.UserManager.Scopes.Commands.Update;
using SibSIU.Domain.UserManager.Scopes.Queries.GetDetails;
using SibSIU.Domain.UserManager.Scopes.Queries.GetPage;
using SibSIU.Domain.UserManager.Scopes.Queries.GetSelectList;
using SibSIU.Identity.Models.Scopes;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ScopeController(
    ICreateScopeHandler create,
    IUpdateScopeHandler update,
    IDeleteScopeHandler delete,
    IGetScopeDetailsHandler getDetails,
    IGetScopeSelectListHandler getSelectList,
    IGetScopePageHandler getPage) : ControllerBase
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
    public async Task<IActionResult> Create(ScopeDetails scope, CancellationToken cancellationToken)
    {
        var result = await create.Handle(new(scope.Id, scope.Name), cancellationToken);
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
    public async Task<IActionResult> Update(ScopeDetails claimType, CancellationToken cancellationToken)
    {
        var result = await update.Handle(new(claimType.Id, claimType.Name), cancellationToken);
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
    public async Task<IActionResult> Create(Ulid scopeId, CancellationToken cancellationToken)
    {
        var result = await delete.Handle(new(scopeId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{scopeId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ScopeDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(Ulid scopeId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(scopeId), cancellationToken);
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
    [ProducesResponseType(typeof(List<ScopeItem>), StatusCodes.Status200OK)]
    public async Task<List<ScopeItem>> GetDetails(string filter, CancellationToken cancellationToken)
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
    [ProducesResponseType(typeof(PaginationList<ScopeRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetScopePageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }
}
