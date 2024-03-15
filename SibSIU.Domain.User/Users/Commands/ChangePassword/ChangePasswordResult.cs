using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePassword;
public sealed class ChangePasswordResult : BaseInnerResult
{
    public ChangePasswordResult(bool emailConfirmed, string email)
        : base(emailConfirmed, email)
    {
    }

    public ChangePasswordResult() : this(false, string.Empty) { }
}
