using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using SibSIU.Domain.ExternalApplication.CORSes.Queries.GetSelectList;

namespace SibSIU.Domain.ExternalApplication.CORSes.Validations;
public sealed class CORSMiddleware(
    IOptions<CorsOptions> CorsOptions,
    IGetCORSSelectListHandler getList) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (CorsOptions.Value is not null)
        {
            var policy = CorsOptions.Value
                .GetPolicy(CorsOptions.Value.DefaultPolicyName);
            if (policy is not null)
            {
                var listOrigins = await getList.Handle(new(), CancellationToken.None);
                string[] origins = listOrigins.Select(o => o.Origin).ToArray();
                if (origins?.Length > 0)
                {
                    var policyOrigins = policy.Origins;
                    var newOrigins = origins.Except(policyOrigins);
                    foreach (var origin in newOrigins)
                        policy.Origins.Add(origin);
                }
            }
        }

        await next(context);
    }
}
