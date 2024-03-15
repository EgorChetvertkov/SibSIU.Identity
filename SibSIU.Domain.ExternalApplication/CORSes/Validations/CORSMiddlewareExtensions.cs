using Microsoft.AspNetCore.Builder;

namespace SibSIU.Domain.ExternalApplication.CORSes.Validations;
public static class CORSMiddlewareExtensions
{
    public static void UseCORSPolicy(this IApplicationBuilder app)
    {
        app.UseMiddleware<CORSMiddleware>();
        app.UseCors();
    }
}
