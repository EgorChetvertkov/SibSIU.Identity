using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;
public interface ISynchronizationWithDeanHandler
    : IRequestHandler<SynchronizationWithDeanRequest, Result<Message>>
{
}
