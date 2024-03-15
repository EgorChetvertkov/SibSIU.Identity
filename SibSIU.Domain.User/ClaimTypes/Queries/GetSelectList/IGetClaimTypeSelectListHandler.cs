using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.ClaimTypes;

namespace SibSIU.Domain.UserManager.ClaimTypes.Queries.GetSelectList;
public interface IGetClaimTypeSelectListHandler : IRequestHandler<GetClaimTypeSelectListRequest, List<ClaimTypeItem>>
{
}
