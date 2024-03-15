using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SibSIU.Auth.Database;
public static class AuthServiceExtensions
{
    /// <summary>
    /// Add connection to dean database with PostgreSQL
    /// </summary>
    /// <param name="services">IServiceCollection for adding services in DI</param>
    /// <param name="connectionString">Connection string for PostgreSQL</param>
    /// <returns>IServiceCollection with adding DeanContext use Npgsql</returns>
    public static IServiceCollection AddAuthDatabase(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddDbContext<AuthContext>(options =>
            options.UseNpgsql(connectionString));
    }

    /// <summary>
    /// Add connection to dean database with PostgreSQL as Factory.
    /// Use for Blazor Server mode. Read https://learn.microsoft.com/En-Us/aspnet/core/blazor/blazor-ef-core?view=aspnetcore-8.0
    /// </summary>
    /// <param name="services">IServiceCollection for adding services in DI</param>
    /// <param name="connectionString">Connection string for PostgreSQL</param>
    /// <returns>IServiceCollection with adding DeanContext use Npgsql as Factory</returns>
    public static IServiceCollection AddAuthDatabaseFactory(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddDbContextFactory<AuthContext>(options =>
            options.UseNpgsql(connectionString));
    }
}
