using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Users.Commands.ChangeBirthday;
using SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;
using SibSIU.Domain.UserManager.Users.Commands.ChangeFIO;
using SibSIU.Domain.UserManager.Users.Commands.ChangePassword;
using SibSIU.Domain.UserManager.Users.Commands.ChangePhone;
using SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;
using SibSIU.Identity.Models.User.Manage.Update;
using SibSIU.Identity.Infrastructure;

using System.Net.Mime;
using SibSIU.Core.Names;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SelfController(
    IChangeUserNameHandler changeUserName,
    IChangeBirthdayHandler changeBirthday,
    IChangeEmailHandler changeEmail,
    IChangeFIOHandler changeFIO,
    IChangePasswordHandler changePassword,
    IChangePhoneHandler changePhone
    ) : ControllerBase
{
    [HttpPatch("change_user_name")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeUserName(ChangeUserNameData userData, CancellationToken cancellationToken)
    {
        var result = await changeUserName.Handle(new(User.GetUserId(ClaimNames.Id), userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("change_birthday")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeBirthday(ChangeBirthdayData userData, CancellationToken cancellationToken)
    {
        var result = await changeBirthday.Handle(new(User.GetUserId(ClaimNames.Id), userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("change_email")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeEmail(ChangeEmailData userData, CancellationToken cancellationToken)
    {
        var result = await changeEmail.Handle(new(User.GetUserId(ClaimNames.Id), userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("change_fio")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeFIO(ChangeFIOData userData, CancellationToken cancellationToken)
    {
        var result = await changeFIO.Handle(new(User.GetUserId(ClaimNames.Id), userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("change_password")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePassword(ChangePasswordData userData, CancellationToken cancellationToken)
    {
        var result = await changePassword.Handle(new(User.GetUserId(ClaimNames.Id), userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("change_phone")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePhone(ChangePhoneData userData, CancellationToken cancellationToken)
    {
        var result = await changePhone.Handle(new(User.GetUserId(ClaimNames.Id), userData), cancellationToken);
        return result.MapToActionResult();
    }
}