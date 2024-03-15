using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Posts.Commands.Delete;

public interface IDeletePostHandler : IRequestHandler<DeletePostRequest, Result<Message>>
{
}