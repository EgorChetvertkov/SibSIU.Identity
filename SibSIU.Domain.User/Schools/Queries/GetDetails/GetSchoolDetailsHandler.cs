using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using SibSIU.Auth.Database;
using SibSIU.Core.Services.Extensions;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Identity.Models.Schools;

namespace SibSIU.Domain.UserManager.Schools.Queries.GetDetails;
public sealed class GetSchoolDetailsHandler(
    ILogger<GetSchoolDetailsHandler> logger,
    IMemoryCache memory,
    AuthContext auth) : IGetSchoolDetailsHandler
{
    public async Task<Result<SchoolDetails>> Handle(GetSchoolDetailsRequest request, CancellationToken cancellationToken)
    {
        return await memory.WithMemoryCache($"school_{request.Id}", 30, request,
            async (request) => await InnerHandle(request, cancellationToken));
    }

    private async Task<Result<SchoolDetails>> InnerHandle(GetSchoolDetailsRequest request, CancellationToken cancellationToken)
    {
        SchoolDetails? details = await auth.Schools
            .Where(p => p.Id == request.Id)
            .Select(p => new SchoolDetails()
            {
                Id = p.Id,
                FullName = p.FullName,
                ShortName = p.ShortName,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (details is null)
        {
            logger.LogInformation("School not found with id {Id}", request.Id);
            return CreateResult.Failure<SchoolDetails>(SchoolErrors.SchoolNotFound);
        }

        return CreateResult.Success(details);
    }
}
