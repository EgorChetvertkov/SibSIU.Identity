using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Units.Commands._Shared;
public sealed class CreateOrUpdateUnitRequest : IRequest<Result<Message>>, IValidated
{
    public Ulid Id { get; }
    public string FullName { get; }
    public string ShortName { get; }
    public Ulid ParentId { get; }

    public CreateOrUpdateUnitRequest(Ulid id, string fullName, string shortName, Ulid parentId)
    {
        Id = id;
        FullName = fullName.TrimOrEmpty();
        ShortName = shortName.TrimOrEmpty();
        ParentId = parentId;
    }

    public CreateOrUpdateUnitRequest() : this(Ulid.Empty, string.Empty, string.Empty, Ulid.Empty) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(FullName))
        {
            return UnitErrors.UnitFullNameEmpty;
        }

        if (string.IsNullOrWhiteSpace(ShortName))
        {
            return UnitErrors.UnitShortNameEmpty;
        }

        if (ParentId == Ulid.Empty)
        {
            return UnitErrors.UnitParentIdEmpty;
        }

        return Error.None;
    }
}
