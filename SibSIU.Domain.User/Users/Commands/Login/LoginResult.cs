using System.Security.Claims;

namespace SibSIU.Domain.UserManager.Users.Commands.Login;
public sealed class LoginResult
{
    public bool IsWarning { get; }
    public string Message { get; }
    public ClaimsPrincipal Principal { get; }

    private LoginResult(bool isWarning, string message, ClaimsPrincipal principal)
    {
        IsWarning = isWarning;
        Message = message;
        Principal = principal;
    }

    public LoginResult() : this(true, string.Empty, new()) { }

    public static LoginResult GetWarning(string message, ClaimsPrincipal principal)
    {
        return new(true, message, principal);
    }

    public static LoginResult GetSuccess(ClaimsPrincipal principal)
    {
        return new(false, "Вы успешно вошли в систему", principal);
    }
}
