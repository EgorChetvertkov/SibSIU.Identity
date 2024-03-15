namespace SibSIU.Domain.UserManager.Utils;
public abstract class BaseInnerResult
{
    public bool EmailConfirmed { get; }
    public string Email { get; }

    public BaseInnerResult(bool emailConfirmed, string email)
    {
        EmailConfirmed = emailConfirmed;
        Email = email;
    }

    public BaseInnerResult() : this(false, string.Empty) { }
}
