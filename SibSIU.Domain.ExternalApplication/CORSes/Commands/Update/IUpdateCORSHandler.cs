using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.CORSes.Commands.Update;

public interface IUpdateCORSHandler : IRequestHandler<UpdateCORSRequest, Result<Message>>
{
}