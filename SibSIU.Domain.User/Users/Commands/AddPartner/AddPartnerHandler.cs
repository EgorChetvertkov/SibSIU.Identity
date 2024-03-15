using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Auth.Database.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.UserData.Database.Entities;
using SibSIU.Core.Services.Extensions;

namespace SibSIU.Domain.UserManager.Users.Commands.AddPartner;
public sealed class AddPartnerHandler(
    ILogger<AddPartnerHandler> logger,
    AuthContext auth) : IAddPartnerHandler
{
    public async Task<Result<Message>> Handle(AddPartnerRequest request, CancellationToken cancellationToken)
    {
        return await request.Ensure(async (request) =>
            await auth.WithTransaction(logger, request,
                (request) => InnerHandler(request, cancellationToken)));
    }

    private async Task<Result<Message>> InnerHandler(AddPartnerRequest request, CancellationToken cancellationToken)
    {
        User? user = await auth.Users.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.UserNotFound);
        }

        int countPartner = await auth.Partners
            .Where(p => p.UserId == request.UserId)
            .CountAsync(cancellationToken);
        if (countPartner != 0)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(UserErrors.CountActivePartnersMustBeZeroForAddNew);
        }

        Organization? organization = await auth.Organizations.GetById(request.OrganizationId, cancellationToken);
        if (organization is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(OrganizationErrors.OrganizationNotFound);
        }

        Post? post = await auth.Posts.GetById(request.PostId, cancellationToken);
        if (post is null)
        {
            auth.Rollback();
            return CreateResult.Failure<Message>(PostErrors.PostNotFound);
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        Partner partner = new()
        {
            Id = Ulid.NewUlid(now),
            CreateAt = now,
            UpdateAt = now,
            IsActive = true,
            Organization = organization,
            Post = post,
            User = user,
        };

        await auth.Partners.AddAsync(partner, cancellationToken);
        await auth.SaveChangesAsync(cancellationToken);

        return CreateResult.Success(new Message("Партнер добавлен"));
    }
}
