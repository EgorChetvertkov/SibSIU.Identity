using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.Schools;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetSelectList;

public interface IGetSchoolSelectListHandler : IRequestHandler<GetSchoolSelectListRequest, List<SchoolItem>>
{
}