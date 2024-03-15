using SibSIU.Domain.UserManager.Utils;

namespace SibSIU.Domain.UserManager.Users.Commands.CreateUser;
public sealed class CreateUserResult : BaseInnerResult
{
    public string UserId { get; set; }
    public CreateUserResult(bool emailConfirmed, string email, Ulid userId)
        : base(emailConfirmed, email)
    {
        UserId = userId.ToString();
    }

    public CreateUserResult() : this(false, string.Empty, Ulid.Empty) { }
}
