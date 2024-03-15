using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.Applications.Commands.Delete;

public interface IDeleteApplicationHandler : IRequestHandler<DeleteApplicationRequest, Result<Message>>
{
}