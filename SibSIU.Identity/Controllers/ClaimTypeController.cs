using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.ClaimTypes.Commands.Create;
using SibSIU.Domain.UserManager.ClaimTypes.Commands.Delete;
using SibSIU.Domain.UserManager.ClaimTypes.Commands.Update;
using SibSIU.Domain.UserManager.ClaimTypes.Queries.GetDetails;
using SibSIU.Domain.UserManager.ClaimTypes.Queries.GetPage;
using SibSIU.Domain.UserManager.ClaimTypes.Queries.GetSelectList;
using SibSIU.Identity.Models.ClaimTypes;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClaimTypeController(
    ICreateClaimTypeHandler create,
    IUpdateClaimTypeHandler update,
    IDeleteClaimTypeHandler delete,
    IGetClaimTypeDetailsHandler getDetails,
    IGetClaimTypeSelectListHandler getSelectList,
    IGetClaimTypePageHandler getPage) : ControllerBase
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
    public async Task<IActionResult> Create(ClaimTypeDetails claimType, CancellationToken cancellationToken)
    {
        var result = await create.Handle(new(claimType.Id, claimType.Name, claimType.IncludeInAccessToken, claimType.IncludeInIdentityToken, claimType.Scopes.Select(s => s.Id).ToList()), cancellationToken);
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
    public async Task<IActionResult> Update(ClaimTypeDetails claimType, CancellationToken cancellationToken)
    {
        var result = await update.Handle(new(claimType.Id, claimType.Name, claimType.IncludeInAccessToken, claimType.IncludeInIdentityToken, claimType.Scopes.Select(s => s.Id).ToList()), cancellationToken);
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
    public async Task<IActionResult> Create(Ulid claimTypeId, CancellationToken cancellationToken)
    {
        var result = await delete.Handle(new(claimTypeId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{claimTypeId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ClaimTypeDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(Ulid claimTypeId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(claimTypeId), cancellationToken);
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
    [ProducesResponseType(typeof(List<ClaimTypeItem>), StatusCodes.Status200OK)]
    public async Task<List<ClaimTypeItem>> GetDetails(string filter, CancellationToken cancellationToken)
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
    [ProducesResponseType(typeof(PaginationList<ClaimTypeRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetClaimTypePageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }
}
