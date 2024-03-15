using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.Applications;

namespace SibSIU.Domain.ExternalApplication.Applications.Queries.GetDetails;

public interface IGetApplicationDetailsHandler : IRequestHandler<GetApplicationDetailsRequest, Result<ApplicationDetails>>
{
}