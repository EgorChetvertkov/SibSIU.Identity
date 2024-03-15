using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;
public sealed class SubmitRegistrationResult(bool emailConfirmed, string email, string message, string userId)
    : BaseInnerResult(emailConfirmed, email)
{
    public SubmitRegistrationResult() : this(false, string.Empty, string.Empty, string.Empty) { }

    public string UserId => userId;
    public string Message => message;
}
