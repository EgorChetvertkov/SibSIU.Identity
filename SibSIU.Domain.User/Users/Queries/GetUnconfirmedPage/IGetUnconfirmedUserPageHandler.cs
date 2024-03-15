using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedPage;

public interface IGetUnconfirmedUserPageHandler : IRequestHandler<GetUnconfirmedUserPageRequest, Result<PaginationList<UnconfirmedUserRowItem>>>
{
}