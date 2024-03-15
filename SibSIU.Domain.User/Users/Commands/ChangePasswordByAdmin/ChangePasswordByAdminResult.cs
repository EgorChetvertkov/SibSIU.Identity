using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePasswordByAdmin;
public sealed class ChangePasswordByAdminResult : BaseInnerResult
{
    public ChangePasswordByAdminResult(bool emailConfirmed, string email)
        : base(emailConfirmed, email)
    {
    }

    public ChangePasswordByAdminResult() : this(false, string.Empty) { }
}
