using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Genders;
using SibSIU.Identity.Models.Scopes;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Queries.GetDetails;
public sealed class GetUserDetailsHandler(
    ILogger<GetUserDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUserDetailsHandler
{
    public async Task<Result<UserDetails>> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"user_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<UserDetails>> InnerHandle(GetUserDetailsRequest request, CancellationToken cancellationToken)
    {
        UserDetails? userDetails = await auth.Users.GetUserByIdMapTo(request.Id, GetUserDetails, cancellationToken);

        if (userDetails is null)
        {
            logger.LogInformation("User not found with id {userId}", request.Id);
            return CreateResult.Failure<UserDetails>(UserErrors.UserNotFound);
        }

        return CreateResult.Success(userDetails);
    }

    private static UserDetails GetUserDetails(User u)
    {
        GenderItem gender = u.Gender is null ?
            new() :
            new(u.Gender.Id, u.Gender.Name);

        List<PartnerDetails> partners = u.Partners.Select(p => new PartnerDetails(
            p.Id,
            new(p.Organization.Id, p.Organization.FullName, p.Organization.ShortName),
            new(p.Post.Id, p.Post.Name))).ToList();

        List<PupilDetails> pupils = u.Pupils.Select(p => new PupilDetails(
            p.Id,
            new(p.School.Id, p.School.FullName, p.School.ShortName), p.ClassLitter, p.ClassNumber))
            .ToList();

        List<WorkDetails> works = u.WorkPlaces.Select(wp => new WorkDetails(
            wp.Id,
            new(wp.Unit.Id, wp.Unit.FullName, wp.Unit.ShortName),
            new(wp.Post.Id, wp.Post.Name))).ToList();

        List<StudentDetails> students = u.Students.Select(s => new StudentDetails(
            s.Id,
            new(s.Group.Name))).ToList();

        List<ClaimDetails> claims = u.Claims.Select(c => new ClaimDetails(
            c.Id,
            c.ClaimType.Settings.Select(s => new ScopeItem(s.Scope.Id, s.Scope.Name)).ToList(),
            new(c.ClaimType.Id, c.ClaimType.Name),
            c.Value)).ToList();

        List<RoleDetails> roles = u.UserRoles.Select(ur => new RoleDetails(
            ur.Role.Id,
            ur.Role.Name
            )).ToList();

        return new UserDetails(
            u.Id,
            u.UserName,
            u.Email,
            u.EmailConfirmed,
            u.PhoneNumber ?? string.Empty,
            u.FirstName,
            u.LastName,
            u.Patronymic ?? string.Empty,
            u.BirthOfDate ?? DateTimeOffset.MinValue,
            gender,
            partners,
            pupils,
            works,
            students,
            claims,
            roles);
    }
}
