using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePhone;
public sealed class ChangePhoneResult : BaseInnerResult
{
    public ChangePhoneResult(bool emailConfirmed, string email)
        : base(emailConfirmed, email)
    {
    }

    public ChangePhoneResult() : this(false, string.Empty) { }
}
