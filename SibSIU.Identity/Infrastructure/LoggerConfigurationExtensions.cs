using Serilog;

namespace SibSIU.Identity.Infrastructure;

public static class LoggerConfigurationExtensions
{
    internal static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(logger);
    }
}
