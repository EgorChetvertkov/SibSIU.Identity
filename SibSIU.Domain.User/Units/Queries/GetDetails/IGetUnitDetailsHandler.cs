using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Units;

namespace SibSIU.Domain.UserManager.Units.Queries.GetDetails;

public interface IGetUnitDetailsHandler : IRequestHandler<GetUnitDetailsRequest, Result<UnitDetails>>
{
}