using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;
public sealed class ChangeEmailResult : BaseInnerResult
{
    public string UserId { get; set; }
    public ChangeEmailResult(bool emailConfirmed, string email, Ulid userId)
        : base(emailConfirmed, email)
    {
        UserId = userId.ToString();
    }

    public ChangeEmailResult() : this(false, string.Empty, Ulid.Empty) { }
}
