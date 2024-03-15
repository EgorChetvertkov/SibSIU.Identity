using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage.Update;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePassword;
public sealed class ChangePasswordRequest :
    IRequest<Result<Message>>,
    IRequest<Result<ChangePasswordResult>>,
    IValidated
{
    public Ulid UserId { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;

    public ChangePasswordRequest(Ulid userId, ChangePasswordData data)
    {
        OldPassword = data.OldPassword;
        NewPassword = data.NewPassword;
        ConfirmNewPassword = data.ConfirmNewPassword;
        UserId = userId;
    }

    public ChangePasswordRequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (OldPassword.Length < 8 || NewPassword.Length < 8)
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
