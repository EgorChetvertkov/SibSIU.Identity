using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Models.User.Manage.Update;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangePhone;
public sealed class ChangePhoneRequest :
    IRequest<Result<Message>>,
    IRequest<Result<ChangePhoneResult>>,
    IValidated
{
    public Ulid UserId { get; set; }
    public string PhoneNumber { get; set; }

    public ChangePhoneRequest(Ulid userId, ChangePhoneData data)
    {
        UserId = userId;
        PhoneNumber = data.PhoneNumber.TrimOrEmpty();
    }

    public ChangePhoneRequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (!PhoneValidation.Validate(PhoneNumber))
        {
            return UserErrors.InvalidPhone;
        }

        return Error.None;
    }
}
