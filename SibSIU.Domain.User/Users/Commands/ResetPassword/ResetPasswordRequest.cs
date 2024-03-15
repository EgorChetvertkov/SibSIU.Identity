using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.ResetPassword;
public sealed class ResetPasswordRequest :
    IRequest<Result<Message>>,
    IValidated
{
    private string _userId;
    public string Code { get; }
    public Ulid UserId => Ulid.Parse(_userId);
    public string NewPassword { get; }
    public string ConfirmedNewPassword { get; }

    public ResetPasswordRequest(string userId, string code, string newPassword, string confirmedNewPassword)
    {
        _userId = userId;
        Code = code;
        NewPassword = newPassword.TrimOrEmpty();
        ConfirmedNewPassword = confirmedNewPassword.TrimOrEmpty();
    }

    public ResetPasswordRequest() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }

    public Error Validate()
    {
        if (Ulid.TryParse(_userId, out var ulid) || ulid == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (string.IsNullOrWhiteSpace(Code))
        {
            return UserErrors.InvalidConfirmationCode;
        }

        if (NewPassword != ConfirmedNewPassword)
        {
            return UserErrors.PasswordNotCompare;
        }

        if (NewPassword.Length < 8)
        {
            return UserErrors.PasswordAreShort;
        }

        return Error.None;
    }
}
