using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.RejectRegistration;
public sealed class RejectRegistrationRequest :
    IRequest<Result<Message>>,
    IRequest<Result<RejectRegistrationResult>>,
    IValidated
{
    public Ulid UserId { get; }
    public string Message { get; }
    public Uri Url { get; }

    public RejectRegistrationRequest(Ulid userId, string message, Uri url)
    {
        UserId = userId;
        Message = message;
        Url = url;
    }

    public RejectRegistrationRequest() : this(Ulid.Empty, string.Empty, new Uri("https://www.sibsiu.ru")) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (string.IsNullOrWhiteSpace(Message))
        {
            return UserErrors.MessageEmpty;
        }

        return Error.None;
    }
}
