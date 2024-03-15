using SibSIU.Core.Services.RequestByFilter;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.CORSes;

namespace SibSIU.Domain.ExternalApplication.CORSes.Queries.GetSelectList;
public sealed class GetCORSSelectListRequest :
    BaseRequestByFilter<CORSItem>,
    IRequest<Result<List<CORSItem>>>
{
    public GetCORSSelectListRequest(string filter)
    {
        Filter = filter;
    }

    public GetCORSSelectListRequest() : this(string.Empty) { }
}
