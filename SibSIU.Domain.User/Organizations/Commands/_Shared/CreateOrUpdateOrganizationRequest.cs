using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Organizations.Commands._Shared;
public sealed class CreateOrUpdateOrganizationRequest
    : IRequest<Result<Message>>, IValidated
{
    public Ulid Id { get; }
    public string FullName { get; }
    public string ShortName { get; }
    public string OGRN { get; }
    public string TIN { get; }
    public string KPP { get; }

    public CreateOrUpdateOrganizationRequest(
        Ulid id,
        string fullName,
        string shortName,
        string oGRN,
        string tIN,
        string kPP)
    {
        Id = id;
        FullName = fullName.TrimOrEmpty();
        ShortName = shortName.TrimOrEmpty();
        OGRN = oGRN.TrimOrEmpty();
        TIN = tIN.TrimOrEmpty();
        KPP = kPP.TrimOrEmpty();
    }

    public CreateOrUpdateOrganizationRequest() : this(
        Ulid.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty)
    { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(FullName))
        {
            return OrganizationErrors.OrganizationFullNameInvalid;
        }

        if (string.IsNullOrWhiteSpace(ShortName))
        {
            return OrganizationErrors.OrganizationShortNameInvalid;
        }

        if (string.IsNullOrWhiteSpace(OGRN) || OGRN.Length != 13)
        {
            return OrganizationErrors.OrganizationOGRNInvalid;
        }

        if (string.IsNullOrWhiteSpace(TIN) || TIN.Length != 10)
        {
            return OrganizationErrors.OrganizationTINInvalid;
        }

        if (string.IsNullOrWhiteSpace(KPP) || KPP.Length != 9)
        {
            return OrganizationErrors.OrganizationKPPInvalid;
        }

        return Error.None;
    }
}
