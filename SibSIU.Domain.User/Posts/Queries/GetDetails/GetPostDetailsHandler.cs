using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Posts;

namespace SibSIU.Domain.UserManager.Posts.Queries.GetDetails;
public sealed class GetPostDetailsHandler(
    ILogger<GetPostDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetPostDetailsHandler
{
    public async Task<Result<PostDetails>> Handle(GetPostDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"post_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<PostDetails>> InnerHandle(GetPostDetailsRequest request, CancellationToken cancellationToken)
    {
        PostDetails? details = await auth.Posts
            .Where(p => p.Id == request.Id)
            .Select(p => new PostDetails()
            {
                Id = p.Id,
                Name = p.Name,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (details is null)
        {
            logger.LogInformation("Post not found with id {Id}", request.Id);
            return CreateResult.Failure<PostDetails>(PostErrors.PostNotFound);
        }

        return CreateResult.Success(details);
    }
}
