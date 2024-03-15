using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SibSIU.CORS.Database;
public static class CORSServiceExtensions
{
    public static IServiceCollection AddCORSDatabase(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddDbContext<CORSContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static IServiceCollection AddAuthDatabaseFactory(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddDbContextFactory<CORSContext>(options =>
            options.UseNpgsql(connectionString));
    }
}
