using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetPage;

public interface IGetUserPageHandler : IRequestHandler<GetUserPageRequest, Result<PaginationList<UserRowItem>>>
{
}