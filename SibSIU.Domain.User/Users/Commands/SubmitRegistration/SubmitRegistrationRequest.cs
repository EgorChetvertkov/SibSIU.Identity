using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;
public sealed class SubmitRegistrationRequest :
    BaseRequestById<Ulid, Message>,
    IRequest<Result<SubmitRegistrationResult>>,
    IValidated
{
    public Uri Url { get; }

    public SubmitRegistrationRequest(Ulid userId, Uri url)
    {
        Id = userId;
        Url = url;
    }

    public SubmitRegistrationRequest() : this(Ulid.Empty, new("https://www.sibsiu.ru")) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? UserErrors.InvalidUserId : Error.None;
    }
}
