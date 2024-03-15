using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.AddPupil;
public sealed class AddPupilRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; set; }
    public int ClassNumber { get; set; }
    public char ClassLitter { get; set; }
    public Ulid SchoolId { get; set; }

    public AddPupilRequest(Ulid userId, int classNumber, char classLitter, Ulid schoolId)
    {
        UserId = userId;
        ClassNumber = classNumber;
        ClassLitter = classLitter;
        SchoolId = schoolId;
    }

    public AddPupilRequest() : this(Ulid.Empty, 0, char.MinValue, Ulid.Empty) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (ClassNumber <= 0 || ClassNumber >= 12)
        {
            return SchoolErrors.InvalidClassNumber;
        }

        if (!Char.IsLetter(ClassLitter))
        {
            return SchoolErrors.InvalidClassLitter;
        }

        return Error.None;
    }
}
