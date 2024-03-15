using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Applications;

namespace SibSIU.Domain.ExternalApplication.Applications.Queries.GetPage;

public interface IGetApplicationPageHandler : IRequestHandler<GetApplicationPageRequest, Result<PaginationList<ApplicationRowItem>>>
{
}