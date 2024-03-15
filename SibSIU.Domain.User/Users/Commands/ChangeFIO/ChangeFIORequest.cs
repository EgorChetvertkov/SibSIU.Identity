using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage.Update;

namespace SibSIU.Domain.UserManager.Users.Commands.ChangeFIO;
public sealed class ChangeFIORequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }

    public ChangeFIORequest(Ulid userId, ChangeFIOData data)
    {
        UserId = userId;
        FirstName = data.FirstName.TrimOrEmpty();
        LastName = data.LastName.TrimOrEmpty();
        Patronymic = data.Patronymic?.Trim();
    }

    public ChangeFIORequest() : this(Ulid.Empty, new()) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (string.IsNullOrWhiteSpace(FirstName))
        {
            return UserErrors.FirstNameAreEmpty;
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            return UserErrors.LastNameAreEmpty;
        }

        return Error.None;
    }
}
