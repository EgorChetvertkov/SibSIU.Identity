using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Scopes;

namespace SibSIU.Domain.UserManager.Scopes.Queries.GetDetails;

public interface IGetScopeDetailsHandler : IRequestHandler<GetScopeDetailsRequest, Result<ScopeDetails>>
{
}