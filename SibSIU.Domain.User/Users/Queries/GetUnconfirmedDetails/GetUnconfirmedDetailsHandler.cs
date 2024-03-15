using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedDetails;
public sealed class GetUnconfirmedDetailsHandler(
    ILogger<GetUnconfirmedDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUnconfirmedDetailsHandler
{
    public async Task<Result<UnconfirmedUserDetails>> Handle(GetUnconfirmedDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"u_user_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<UnconfirmedUserDetails>> InnerHandle(GetUnconfirmedDetailsRequest request, CancellationToken cancellationToken)
    {
        UnconfirmedUserDetails? userDetails = await auth.Users
            .IgnoreQueryFilters()
            .Where(u => !u.IsConfirmedUser && u.IsActive)
            .GetUserByIdMapTo(request.Id, GetUserDetails, cancellationToken);

        if (userDetails is null)
        {
            logger.LogInformation("User not found with id {userId}", request.Id);
            return CreateResult.Failure<UnconfirmedUserDetails>(UserErrors.UserNotFound);
        }

        return CreateResult.Success(userDetails);
    }

    private static UnconfirmedUserDetails GetUserDetails(User u)
    {
        List<PartnerDetails> partners = u.Partners.Select(p => new PartnerDetails(
            p.Id,
            new(p.Organization.Id, p.Organization.FullName, p.Organization.ShortName),
            new(p.Post.Id, p.Post.Name))).ToList();

        List<PupilDetails> pupils = u.Pupils.Select(p => new PupilDetails(
            p.Id,
            new(p.School.Id, p.School.FullName, p.School.ShortName), p.ClassLitter, p.ClassNumber))
            .ToList();

        List<StudentDetails> students = u.Students.Select(s => new StudentDetails(
            s.Id,
            new(s.Group.Name))).ToList();

        return new UnconfirmedUserDetails(
            u.Id,
            u.UserName,
            u.FirstName,
            u.LastName,
            u.Patronymic ?? string.Empty,
            pupils.FirstOrDefault(),
            students.FirstOrDefault(),
            partners.FirstOrDefault());
    }
}
