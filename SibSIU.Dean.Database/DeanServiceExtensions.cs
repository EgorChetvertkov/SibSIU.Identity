using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SibSIU.Dean.Database;
public static class DeanServiceExtensions
{
    /// <summary>
    /// Add connection to dean database with SqlServer
    /// </summary>
    /// <param name="services">IServiceCollection for adding services in DI</param>
    /// <param name="connectionString">Connection string for SqlServer</param>
    /// <returns>IServiceCollection with adding DeanContext use SqlServer</returns>
    public static IServiceCollection AddDeanDatabase(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddDbContext<DeanContext>(options =>
            options.UseSqlServer(connectionString, o => o.UseCompatibilityLevel(80)));
    }

    /// <summary>
    /// Add connection to dean database with SqlServer as Factory.
    /// Use for Blazor Server mode. Read https://learn.microsoft.com/En-Us/aspnet/core/blazor/blazor-ef-core?view=aspnetcore-8.0
    /// </summary>
    /// <param name="services">IServiceCollection for adding services in DI</param>
    /// <param name="connectionString">Connection string for SqlServer</param>
    /// <returns>IServiceCollection with adding DeanContext use SqlServer as Factory</returns>
    public static IServiceCollection AddDeanDatabaseFactory(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddDbContextFactory<DeanContext>(options =>
            options.UseSqlServer(connectionString, o => o.UseCompatibilityLevel(80)));
    }
}
