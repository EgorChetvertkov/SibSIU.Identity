using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;

namespace SibSIU.Domain.UserManager.Posts.Commands.Delete;
public sealed class DeletePostRequest : BaseRequestById<Ulid, Message>, IValidated
{
    public DeletePostRequest(Ulid postId)
    {
        Id = postId;
    }

    public DeletePostRequest() : this(Ulid.Empty) { }

    public Error Validate()
    {
        return Id == Ulid.Empty ? PostErrors.InvalidPostId : Error.None;
    }
}
