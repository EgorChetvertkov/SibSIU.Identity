using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Domain.UserManager.Units.Queries.GetSelectList;

public interface IGetUnitSelectListHandler : IRequestHandler<GetUnitSelectListRequest, List<UnitItem>>
{
}