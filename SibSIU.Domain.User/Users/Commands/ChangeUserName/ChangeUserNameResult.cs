using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;
public sealed class ChangeUserNameResult : BaseInnerResult
{
    public ChangeUserNameResult(bool emailConfirmed, string email)
        : base(emailConfirmed, email)
    {
    }
    
    public ChangeUserNameResult() : this(false, string.Empty) { }
}
