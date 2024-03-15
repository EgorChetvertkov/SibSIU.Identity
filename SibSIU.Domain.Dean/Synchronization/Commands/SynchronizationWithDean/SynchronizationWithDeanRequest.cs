using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;

public sealed class SynchronizationWithDeanRequest : IRequest<Result<Message>>
{
}