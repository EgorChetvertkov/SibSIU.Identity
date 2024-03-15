using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.ConfirmedEmail;
public sealed class ConfirmedEmailRequest : IRequest<Result<Message>>, IValidated
{
    private string _userId;
    public string Code { get; set; }
    public Ulid UserId => Ulid.Parse(_userId);

    public ConfirmedEmailRequest(string userId, string code)
    {
        _userId = userId;
        Code = code;
    }

    public ConfirmedEmailRequest() : this(string.Empty, string.Empty) { }

    public Error Validate()
    {
        if (Ulid.TryParse(_userId, out var ulid) || ulid == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (string.IsNullOrWhiteSpace(Code))
        {
            return UserErrors.InvalidConfirmationCode;
        }

        return Error.None;
    }
}
