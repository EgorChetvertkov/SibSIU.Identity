using Microsoft.AspNetCore.Mvc;

using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.ExternalApplication.Applications.Commands.Create;
using SibSIU.Domain.ExternalApplication.Applications.Commands.Delete;
using SibSIU.Domain.ExternalApplication.Applications.Commands.Update;
using SibSIU.Domain.ExternalApplication.Applications.Queries.GetDetails;
using SibSIU.Domain.ExternalApplication.Applications.Queries.GetPage;
using SibSIU.Identity.Models.Applications;

using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientController(
    ICreateApplicationHandler create,
    IUpdateApplicationHandler update,
    IDeleteApplicationHandler delete,
    IGetApplicationDetailsHandler getDetails,
    IGetApplicationPageHandler getPage) : ControllerBase
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
    public async Task<IActionResult> Create(ApplicationDetails client, CancellationToken cancellationToken)
    {
        var result = await create.Handle(new(
            client.ApplicationType,
            client.ClientId,
            client.ClientType,
            client.ClientSecret ?? string.Empty,
            client.JSONWebKeySet ?? string.Empty,
            client.ConsentType,
            client.DisplayName,
            client.Permissions,
            client.RedirectUris,
            client.PostLogoutRedirectUris,
            client.Requirements,
            client.Settings), cancellationToken);
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
    public async Task<IActionResult> Update(ApplicationDetails client, CancellationToken cancellationToken)
    {
        var result = await update.Handle(new(
            client.ApplicationType,
            client.ClientId,
            client.ClientType,
            client.ClientSecret ?? string.Empty,
            client.JSONWebKeySet ?? string.Empty,
            client.ConsentType,
            client.DisplayName,
            client.Permissions,
            client.RedirectUris,
            client.PostLogoutRedirectUris,
            client.Requirements,
            client.Settings), cancellationToken);
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
    public async Task<IActionResult> Create(string clientId, CancellationToken cancellationToken)
    {
        var result = await delete.Handle(new(clientId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{clientId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApplicationDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(string clientId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(clientId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PaginationList<ApplicationRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetApplicationPageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }
}
