using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SibSIU.Core.Names;
using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Core.Services.ResultObject.Extensions;
using SibSIU.Domain.UserManager.Users.Commands.AddClaim;
using SibSIU.Domain.UserManager.Users.Commands.AddPartner;
using SibSIU.Domain.UserManager.Users.Commands.AddPupil;
using SibSIU.Domain.UserManager.Users.Commands.AddStudent;
using SibSIU.Domain.UserManager.Users.Commands.AddWorkPlace;
using SibSIU.Domain.UserManager.Users.Commands.ChangeBirthday;
using SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;
using SibSIU.Domain.UserManager.Users.Commands.ChangeFIO;
using SibSIU.Domain.UserManager.Users.Commands.ChangePasswordByAdmin;
using SibSIU.Domain.UserManager.Users.Commands.ChangePhone;
using SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;
using SibSIU.Domain.UserManager.Users.Commands.CreateUser;
using SibSIU.Domain.UserManager.Users.Commands.RejectRegistration;
using SibSIU.Domain.UserManager.Users.Commands.RemoveClaim;
using SibSIU.Domain.UserManager.Users.Commands.RemovePartner;
using SibSIU.Domain.UserManager.Users.Commands.RemovePupil;
using SibSIU.Domain.UserManager.Users.Commands.RemoveStudent;
using SibSIU.Domain.UserManager.Users.Commands.RemoveWorkPlace;
using SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;
using SibSIU.Domain.UserManager.Users.Queries.GetDetails;
using SibSIU.Domain.UserManager.Users.Queries.GetPage;
using SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedDetails;
using SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedPage;
using SibSIU.Identity.Models.User.Claims;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.Identity.Models.User.Manage.Update;
using SibSIU.Identity.Models.User.Partner;
using SibSIU.Identity.Models.User.Pupil;
using SibSIU.Identity.Models.User.WorkPlace;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleNames.BaseAdministrator)]
public class UserController(
    IGetUserPageHandler getPage,
    IGetUserDetailsHandler getDetails,
    ICreateUserHandler createUser,
    IChangeUserNameHandler changeUserName,
    IChangeBirthdayHandler changeBirthday,
    IChangeEmailHandler changeEmail,
    IChangeFIOHandler changeFIO,
    IChangePasswordByAdminHandler changePassword,
    IChangePhoneHandler changePhone,
    IAddStudentHandler addStudent,
    IRemoveStudentHandler removeStudent,
    IAddPupilHandler addPupil,
    IRemovePupilHandler removePupil,
    IAddPartnerHandler addPartner,
    IRemovePartnerHandler removePartner,
    IAddWorkPlaceHandler addWorkPlace,
    IRemoveWorkPlaceHandler removeWorkPlace,
    IAddClaimHandler addClaim,
    IRemoveClaimHandler removeClaim,
    ISubmitRegistrationHandler submit,
    IRejectRegistrationHandler reject,
    IGetUnconfirmedUserPageHandler getUnconfirmedPage,
    IGetUnconfirmedDetailsHandler getUnconfirmedDetails) : ControllerBase
{
    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PaginationList<UserRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPage([FromQuery] GetUserPageRequest request, CancellationToken cancellationToken)
    {
        var result = await getPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("{userId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetails(Ulid userId, CancellationToken cancellationToken)
    {
        var result = await getDetails.Handle(new(userId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser(CreateUserData userData, CancellationToken cancellationToken)
    {
        var result = await createUser.Handle(new(userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/change_user_name")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeUserName(Ulid userId, ChangeUserNameData userData, CancellationToken cancellationToken)
    {
        var result = await changeUserName.Handle(new(userId, userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/change_birthday")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeBirthday(Ulid userId, ChangeBirthdayData userData, CancellationToken cancellationToken)
    {
        var result = await changeBirthday.Handle(new(userId, userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/change_email")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeEmail(Ulid userId, ChangeEmailData userData, CancellationToken cancellationToken)
    {
        var result = await changeEmail.Handle(new(userId, userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/change_fio")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeFIO(Ulid userId, ChangeFIOData userData, CancellationToken cancellationToken)
    {
        var result = await changeFIO.Handle(new(userId, userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/change_password")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePassword(Ulid userId, ChangePasswordByAdminData userData, CancellationToken cancellationToken)
    {
        var result = await changePassword.Handle(new(userId, userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/change_phone")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePhone(Ulid userId, ChangePhoneData userData, CancellationToken cancellationToken)
    {
        var result = await changePhone.Handle(new(userId, userData), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/add_student/{deanCode}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddStudent(Ulid userId, int deanCode, CancellationToken cancellationToken)
    {
        var result = await addStudent.Handle(new(userId, deanCode), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/remove_student/{studentId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveStudent(Ulid userId, Ulid studentId, CancellationToken cancellationToken)
    {
        var result = await removeStudent.Handle(new(studentId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/add_pupil")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddPupil(Ulid userId, AddPupilData data, CancellationToken cancellationToken)
    {
        var result = await addPupil.Handle(new(userId, data.ClassNumber, data.ClassLitter, data.SchoolId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/remove_pupil/{pupilId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemovePupil(Ulid userId, Ulid pupilId, CancellationToken cancellationToken)
    {
        var result = await removePupil.Handle(new(pupilId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/add_partner")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddPartner(Ulid userId, AddPartnerData data, CancellationToken cancellationToken)
    {
        var result = await addPartner.Handle(new(userId, data.OrganizationId, data.PostId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/remove_partner/{partnerId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemovePartner(Ulid userId, Ulid partnerId, CancellationToken cancellationToken)
    {
        var result = await removePartner.Handle(new(partnerId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/add_work_place")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddWorkPlace(Ulid userId, AddWorkPlaceData data, CancellationToken cancellationToken)
    {
        var result = await addWorkPlace.Handle(new(userId, data.UnitId, data.PostId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/remove_work_place/{workPlaceId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveWorkPlace(Ulid userId, Ulid workPlaceId, CancellationToken cancellationToken)
    {
        var result = await removeWorkPlace.Handle(new(workPlaceId), cancellationToken);
        return result.MapToActionResult();
    }


    [HttpPatch("{userId}/add_claim")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddClaim(Ulid userId, AddClaimData data, CancellationToken cancellationToken)
    {
        var result = await addClaim.Handle(new(userId, data.ClaimTypeId, data.Value), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPatch("{userId}/remove_claim/{claimId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveClaim(Ulid userId, Ulid claimId, CancellationToken cancellationToken)
    {
        var result = await removeClaim.Handle(new(claimId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("unconfirmed")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PaginationList<UnconfirmedUserRowItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnconfirmedPage([FromQuery] GetUnconfirmedUserPageRequest request, CancellationToken cancellationToken)
    {
        var result = await getUnconfirmedPage.Handle(request, cancellationToken);
        return result.MapToActionResult();
    }

    [HttpGet("unconfirmed/{userId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(UnconfirmedUserDetails), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnconfirmedDetails(Ulid userId, CancellationToken cancellationToken)
    {
        var result = await getUnconfirmedDetails.Handle(new(userId), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("submit/{userId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> SubmitRegistration(Ulid userId, CancellationToken cancellationToken)
    {
        var result = await submit.Handle(new(userId,
            new(Request.Host.Value, UriKind.Absolute)), cancellationToken);
        return result.MapToActionResult();
    }

    [HttpPost("reject/{userId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    public async Task<IActionResult> RejectRegistration(Ulid userId, [FromBody] string message, CancellationToken cancellationToken)
    {
        var result = await reject.Handle(new(userId, message,
            new(Request.Host.Value, UriKind.Absolute)), cancellationToken);
        return result.MapToActionResult();
    }
}
