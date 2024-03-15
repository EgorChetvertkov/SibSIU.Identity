using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Schools;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetDetails;

public interface IGetSchoolDetailsHandler : IRequestHandler<GetSchoolDetailsRequest, Result<SchoolDetails>>
{
}