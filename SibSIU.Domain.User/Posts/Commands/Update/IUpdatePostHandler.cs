using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Posts.Commands._Shared;

namespace SibSIU.Domain.UserManager.Posts.Commands.Update;

public interface IUpdatePostHandler : IRequestHandler<CreateOrUpdatePostRequest, Result<Message>>
{
}