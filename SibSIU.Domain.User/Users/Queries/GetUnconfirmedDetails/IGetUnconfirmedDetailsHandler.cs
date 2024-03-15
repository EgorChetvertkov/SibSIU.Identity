using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Identity.Models.User.Manage;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedDetails;

public interface IGetUnconfirmedDetailsHandler : IRequestHandler<GetUnconfirmedDetailsRequest, Result<UnconfirmedUserDetails>>
{
}