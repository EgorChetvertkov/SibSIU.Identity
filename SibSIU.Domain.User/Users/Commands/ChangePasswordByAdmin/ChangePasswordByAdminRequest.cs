using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage.Update;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePasswordByAdmin;
public sealed class ChangePasswordByAdminRequest :
    IRequest<Result<Message>>,
    IRequest<Result<ChangePasswordByAdminResult>>,
    IValidated
{
    public Ulid UserId { get; set; }
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;

    public ChangePasswordByAdminRequest(Ulid userId, ChangePasswordByAdminData data)
    {
        NewPassword = data.NewPassword;
        ConfirmNewPassword = data.ConfirmNewPassword;
        UserId = userId;
    }

    public ChangePasswordByAdminRequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (NewPassword.Length < 8)
        {
            return UserErrors.PasswordAreShort;
        }

        if (NewPassword != ConfirmNewPassword)
        {
            return UserErrors.PasswordNotCompare;
        }

        return Error.None;
    }
}
