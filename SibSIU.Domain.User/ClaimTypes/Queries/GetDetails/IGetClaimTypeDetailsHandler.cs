using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetDetails;

public interface IGetClaimTypeDetailsHandler : IRequestHandler<GetClaimTypeDetailsRequest, Result<ClaimTypeDetails>>
{
}