using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.User.Students;

namespace SibSIU.Domain.UserManager.Users.Queries.GetStudentsWithoutUser;
public sealed class GetStudentsWithoutUserRequest : IRequest<List<StudentItem>>
{
    public string GroupName { get; }

    public GetStudentsWithoutUserRequest(string groupName)
    {
        GroupName = groupName;
    }

    public GetStudentsWithoutUserRequest() : this(string.Empty) { }
}
