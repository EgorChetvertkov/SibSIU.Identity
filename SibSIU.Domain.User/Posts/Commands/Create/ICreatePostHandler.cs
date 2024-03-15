using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Posts.Commands._Shared;

namespace SibSIU.Domain.UserManager.Posts.Commands.Create;

public interface ICreatePostHandler : IRequestHandler<CreateOrUpdatePostRequest, Result<Message>>
{
}