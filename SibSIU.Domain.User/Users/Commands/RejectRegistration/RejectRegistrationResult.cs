using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.RejectRegistration;
public sealed class RejectRegistrationResult(bool emailConfirmed, string email, string message)
    : BaseInnerResult(emailConfirmed, email)
{
    public RejectRegistrationResult() : this(false, string.Empty, string.Empty) { }

    public string Message => message;
}
