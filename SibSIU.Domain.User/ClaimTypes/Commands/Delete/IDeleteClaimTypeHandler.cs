using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.ClaimTypes.Commands.Delete;

public interface IDeleteClaimTypeHandler : IRequestHandler<DeleteClaimTypeRequest, Result<Message>>
{
}