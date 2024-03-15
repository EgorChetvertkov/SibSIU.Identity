using SibSIU.Core.Services.RequestById;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands.Delete;
public sealed class DeleteApplicationRequest : BaseRequestById<ValueString, Message>
{
    public DeleteApplicationRequest(string id)
    {
        Id = new(id);
    }

    public DeleteApplicationRequest() : this(string.Empty) { }
}
