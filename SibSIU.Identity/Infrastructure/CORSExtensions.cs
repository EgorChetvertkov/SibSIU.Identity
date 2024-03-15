namespace SibSIU.Identity.Infrastructure;

public static class CORSExtensions
{
    public static string SectionName { get; set; } = "CorsOrigins";

    public static void AddCORS(this WebApplicationBuilder builder)
    {
        string[] origins = builder.Configuration.GetSection(SectionName).Get<string[]>() ?? [];

        builder.Services.AddCors(options => options
            .AddDefaultPolicy(b => b
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));
    }
}
