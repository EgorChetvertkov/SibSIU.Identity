using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Genders;
using SibSIU.Identity.Models.User.Manage;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.UserManager.Users.Queries.GetUserInfoByUserName;
public sealed class GetUserInfoByUserNameHandler(
    ILogger<GetUserInfoByUserNameHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetUserInfoByUserNameHandler
{
    public async Task<Result<UserDetails>> Handle(GetUserInfoByUserNameRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await memory.WithMemoryCache($"user_{request.UserName}", 30, request,
                async (request) => await InnerHandle(request, cancellationToken)));
    }

    private async Task<Result<UserDetails>> InnerHandle(GetUserInfoByUserNameRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users
            .Where(u => u.UserName == request.UserName)
            .Include(u => u.Students)
                .ThenInclude(s => s.Group)
            .Include(u => u.WorkPlaces)
                .ThenInclude(wp => wp.Unit)
            .Include(u => u.WorkPlaces)
                .ThenInclude(wp => wp.Post)
            .Include(u => u.Gender)
            .Include(u => u.Partners)
                .ThenInclude(p => p!.Organization)
            .Include(u => u.Partners)
                .ThenInclude(p => p!.Post)
            .Include(u => u.Pupils)
                .ThenInclude(p => p!.School)
            .Include(u => u.Claims)
                .ThenInclude(c => c.ClaimType)
            .Include(u => u.Claims)
                .ThenInclude(c => c.Scope)
            .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            logger.LogInformation("User not found with id {userName}", request.UserName);
            return CreateResult.Failure<UserDetails>(UserErrors.UserNotFound);
        }

        return CreateResult.Success(GetUserDetails(user));
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
            new(c.Scope.Id, c.Scope.Name),
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
