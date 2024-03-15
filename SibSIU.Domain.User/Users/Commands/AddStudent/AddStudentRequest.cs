using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Users.Commands.AddStudent;
public sealed class AddStudentRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid UserId { get; set; }
    public int DeanCode { get; set; }

    public AddStudentRequest(Ulid userId, int deanCode)
    {
        UserId = userId;
        DeanCode = deanCode;
    }

    public AddStudentRequest() : this(Ulid.Empty, -1) { }

    public Error Validate()
    {
        if (UserId == Ulid.Empty)
        {
            return UserErrors.InvalidUserId;
        }

        if (DeanCode <= 0)
        {
            return StudentErrors.InvalidStudentDeanCode;
        }

        return Error.None;
    }
}
