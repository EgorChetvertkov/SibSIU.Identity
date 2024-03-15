using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Schools;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetPage;

public interface IGetSchoolPageHandler : IRequestHandler<GetSchoolPageRequest, Result<PaginationList<SchoolRowItem>>>
{
}