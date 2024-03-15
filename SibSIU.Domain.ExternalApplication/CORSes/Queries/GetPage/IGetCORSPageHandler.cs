using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetPage;

public interface IGetCORSPageHandler : IRequestHandler<GetCORSPageRequest, Result<PaginationList<CORSRowItem>>>
{
}