using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Organizations.Commands.Create;
using SibSIU.Domain.UserManager.Organizations.Commands.Delete;
using SibSIU.Domain.UserManager.Organizations.Commands.Update;
using SibSIU.Domain.UserManager.Organizations.Queries.GetDetails;
using SibSIU.Domain.UserManager.Organizations.Queries.GetPage;
using SibSIU.Domain.UserManager.Organizations.Queries.GetSelectList;
using SibSIU.Identity.Models.Organizations;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrganizationController(
    ICreateOrganizationHandler create,
    IUpdateOrganizationHandler update,
    IDeleteOrganizationHandler delete,
    IGetOrganizationDetailsHandler getDetails,
    IGetOrganizationSelectListHandler getSelectList,
    IGetOrganizationPageHandler getPage) : ControllerBase
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
    public async Task<IActionResult> Create(OrganizationDetails organization, CancellationToken cancellationToken)
    {
        var result = await create.Handle(new(
            organization.Id,
            organization.FullName,
            organization.ShortName,
            organization.OGRN,
            organization.TIN,
            organization.KPP), cancellationToken);
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
    public async Task<IActionResult> Update(OrganizationDetails organization, CancellationToken cancellationToken)
    {
        var result = await update.Handle(new(
            organization.Id,
            organization.FullName,
            organization.ShortName,
            organization.OGRN,
            organization.TIN,
            organization.KPP), cancellationToken);
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
    public async Task<IActionResult> Create(Ulid organizationId, CancellationToken cancellationToken)
    {
        var result = await delete.Handle(new(organizationId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{organizationId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(OrganizationDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(Ulid organizationId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(organizationId), cancellationToken);
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
    [ProducesResponseType(typeof(List<OrganizationItem>), StatusCodes.Status200OK)]
    public async Task<List<OrganizationItem>> GetDetails(string filter, CancellationToken cancellationToken)
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
    [ProducesResponseType(typeof(PaginationList<OrganizationRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetOrganizationPageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }
}
