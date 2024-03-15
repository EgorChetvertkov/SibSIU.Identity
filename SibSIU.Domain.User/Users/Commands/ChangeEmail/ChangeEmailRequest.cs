using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage.Update;

using System.Net.Mail;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;
public sealed class ChangeEmailRequest
    : IRequest<Result<Message>>,
    IRequest<Result<ChangeEmailResult>>,
    IValidated
{
    public Ulid UserId { get; }
    public string Email { get; }

    public ChangeEmailRequest(Ulid userId, ChangeEmailData data)
    {
        UserId = userId;
        Email = data.Email.TrimOrEmpty();
    }

    public ChangeEmailRequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }
        
        if (string.IsNullOrEmpty(Email) || !MailAddress.TryCreate(Email, out _))
        {
            return UserErrors.InvalidEmail;
        }

        return Error.None;
    }
}
