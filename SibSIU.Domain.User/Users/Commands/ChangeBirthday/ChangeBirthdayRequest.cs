using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage.Update;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeBirthday;
public sealed class ChangeBirthdayRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; set; }
    public DateTimeOffset Birthday { get; set; }

    public ChangeBirthdayRequest(Ulid userId, ChangeBirthdayData data)
    {
        UserId = userId;
        Birthday = data.Birthday;
    }

    public ChangeBirthdayRequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (Birthday.ToUniversalTime().Date.Year - DateTimeOffset.UtcNow.ToUniversalTime().Date.Year >= 14)
        {
            return UserErrors.AgeAreSmall;
        }

        return Error.None;
    }
}
