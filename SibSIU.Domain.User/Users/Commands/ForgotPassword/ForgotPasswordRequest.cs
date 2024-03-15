using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

using System.Net.Mail;

namespace SibSIU.Domain.UserManager.Users.Commands.ForgotPassword;
public sealed class ForgotPasswordRequest :
    IRequest<Result<Message>>,
    IRequest<Result<ForgotPasswordResult>>,
    IValidated
{
    public string Email { get; }

    public ForgotPasswordRequest(string email)
    {
        Email = email.TrimOrEmpty();
    }

    public ForgotPasswordRequest() : this(string.Empty) { }

    public Error Validate()
    {
        return MailAddress.TryCreate(Email, out _) ? Error.None : UserErrors.InvalidEmail;
    }
}
