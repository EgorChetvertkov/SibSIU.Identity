using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.AcademicGroups;

namespace SibSIU.Domain.UserManager.Groups.Queries.GetSelectList;

public interface IGetGroupSelectListHandler : IRequestHandler<GetGroupSelectListRequest, List<AcademicGroupItem>>
{
}