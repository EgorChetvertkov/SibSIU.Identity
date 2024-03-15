using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetDetails;

public interface IGetPostDetailsHandler : IRequestHandler<GetPostDetailsRequest, Result<PostDetails>>
{
}