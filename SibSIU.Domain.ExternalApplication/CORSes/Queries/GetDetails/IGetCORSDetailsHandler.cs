using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetDetails;

public interface IGetCORSDetailsHandler : IRequestHandler<GetCORSDetailsRequest, Result<CORSDetails>>
{
}