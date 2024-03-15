using SibSIU.Core.Services.Pagination;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetPage;

public interface IGetPostPageHandler
    : IRequestHandler<GetPostPageRequest, Result<PaginationList<PostRowItem>>>
{
}