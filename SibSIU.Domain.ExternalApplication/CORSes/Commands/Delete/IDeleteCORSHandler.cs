using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Delete;

public interface IDeleteCORSHandler : IRequestHandler<DeleteCORSRequest, Result<Message>>
{
}