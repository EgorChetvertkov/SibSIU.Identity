using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Create;

public interface ICreateCORSHandler : IRequestHandler<CreateCORSRequest, Result<Message>>
{
}