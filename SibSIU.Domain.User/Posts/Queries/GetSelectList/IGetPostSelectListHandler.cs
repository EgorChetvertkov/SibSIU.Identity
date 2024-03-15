using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetSelectList;

public interface IGetPostSelectListHandler : IRequestHandler<GetPostSelectListRequest, List<PostItem>>
{
}