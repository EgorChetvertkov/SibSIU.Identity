using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Domain.UserManager.Units.Queries.GetPage;

public interface IGetUnitPageHandler : IRequestHandler<GetUnitPageRequest, Result<UnitRowItem>>
{
}