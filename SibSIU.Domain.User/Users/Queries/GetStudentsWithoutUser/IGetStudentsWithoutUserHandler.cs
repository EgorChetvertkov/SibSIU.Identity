using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Identity.Models.User.Students;

namespace SibSIU.Domain.UserManager.Users.Queries.GetStudentsWithoutUser;

public interface IGetStudentsWithoutUserHandler : IRequestHandler<GetStudentsWithoutUserRequest, List<StudentItem>>
{
}