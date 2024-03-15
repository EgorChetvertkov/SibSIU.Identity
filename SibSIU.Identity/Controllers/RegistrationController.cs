using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Users.Commands.RegisterAsPartner;
using SibSIU.Domain.UserManager.Users.Commands.RegisterAsPupil;
using SibSIU.Domain.UserManager.Users.Commands.RegisterAsStudent;
using SibSIU.Identity.Models.User.Register;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class RegistrationController(
    IRegisterAsPartnerHandler registerAsPartner,
    IRegisterAsPupilHandler registerAsPupil,
    IRegisterAsStudentHandler registerAsStudent) : ControllerBase
{
    [HttpPost("as/partner")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AsPartner(RegisterAsPartnerData data, CancellationToken cancellationToken)
    {
        var result = await registerAsPartner.Handle(new(data), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("as/pupil")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AsPartner(RegisterAsPupilData data, CancellationToken cancellationToken)
    {
        var result = await registerAsPupil.Handle(new(data), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("as/student")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AsStudent(RegisterAsStudentData data, CancellationToken cancellationToken)
    {
        var result = await registerAsStudent.Handle(new(data), cancellationToken);
        return result.MapToActionResult();
    }
}
