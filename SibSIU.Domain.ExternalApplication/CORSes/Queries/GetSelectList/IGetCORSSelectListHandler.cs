using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetSelectList;

public interface IGetCORSSelectListHandler : IRequestHandler<GetCORSSelectListRequest, List<CORSItem>>
{
}