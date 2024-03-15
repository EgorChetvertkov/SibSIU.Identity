using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.ForgotPassword;

public sealed class ForgotPasswordResult : BaseInnerResult
{
    public string UserId { get; }
    public string Password { get; }
    public ForgotPasswordResult(bool emailConfirmed, string email, Ulid userId, string password)
        : base(emailConfirmed, email)
    {
        UserId = userId.ToString();
        Password = password;
    }

    public ForgotPasswordResult() : this(false, string.Empty, Ulid.Empty, string.Empty) { }
}