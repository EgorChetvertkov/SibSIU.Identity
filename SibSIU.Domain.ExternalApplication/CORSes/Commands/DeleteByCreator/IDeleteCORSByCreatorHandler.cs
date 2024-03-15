using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.DeleteByCreator;

public interface IDeleteCORSByCreatorHandler : IRequestHandler<DeleteCORSByCreatorRequest, Result<Message>>
{
}