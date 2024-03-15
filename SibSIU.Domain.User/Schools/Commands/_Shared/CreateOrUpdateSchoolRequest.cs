using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Schools.Commands._Shared;
public sealed class CreateOrUpdateSchoolRequest :
    IRequest<Result<Message>>,
    IValidated
{
    public Ulid Id { get; }
    public string FullName { get; }
    public string ShortName { get; }

    public CreateOrUpdateSchoolRequest(Ulid id, string fullName, string shortName)
    {
        Id = id;
        FullName = fullName.TrimOrEmpty();
        ShortName = shortName.TrimOrEmpty();
    }

    public CreateOrUpdateSchoolRequest() : this(Ulid.Empty, string.Empty, string.Empty) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(FullName))
        {
            return SchoolErrors.SchoolFullNameEmpty;
        }

        if (string.IsNullOrWhiteSpace(ShortName))
        {
            return SchoolErrors.SchoolShortNameEmpty;
        }

        return Error.None;
    }
}
