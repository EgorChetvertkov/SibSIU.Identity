using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Quartz;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SibSIU.Identity.Database;
public static class OpenIdConnectServiceExtensions
{
    /// <summary>
    /// Add openiddict library with postgresql database and quartz.
    /// Use endpoints:
    /// connect/authorize
    /// connect/logout
    /// connect/token
    /// connect/userinfo
    /// connect/introspect
    /// connect/device
    /// connect/verify
    /// </summary>
    /// <param name="services">IServiceCollection for adding services in DI</param>
    /// <param name="connectionString">Connections string for postgresql</param>
    /// <param name="encryptionKey">Encryption certificate for encrypt tokens. If null use AddDevelopmentEncryptionCertificate()</param>
    /// <param name="signingKey">Signing certificate for signing tokens. If null use AddDevelopmentSigningCertificate()</param>
    /// <returns>IServiceCollection with setting openididct services</returns>
    public static IServiceCollection AddOpenIdConnectDatabase(
        this IServiceCollection services,
        string connectionString,
        string encryptionKey,
        X509Certificate2 signingKey)
    {
        services.AddDbContext<OpenIdConnectContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseOpenIddict();
        });

        SetUpOpenIdDict(services, encryptionKey, signingKey);

        return services;
    }

    /// <summary>
    /// Add openiddict library with postgresql database as Factory and quartz.
    /// Use for Blazor Server mode. Read https://learn.microsoft.com/En-Us/aspnet/core/blazor/blazor-ef-core?view=aspnetcore-8.0
    /// Use endpoints:
    /// connect/authorize
    /// connect/logout
    /// connect/token
    /// connect/userinfo
    /// connect/introspect
    /// connect/device
    /// connect/verify
    /// </summary>
    /// <param name="services">IServiceCollection for adding services in DI</param>
    /// <param name="connectionString">Connections string for postgresql</param>
    /// <param name="encryptionKey">Encryption certificate for encrypt tokens. If null use AddDevelopmentEncryptionCertificate()</param>
    /// <param name="signingKey">Signing certificate for signing tokens. If null use AddDevelopmentSigningCertificate()</param>
    /// <returns>IServiceCollection with setting openididct services and DbContext as Factory</returns>
    public static IServiceCollection AddOpenIdConnectDatabaseFactory(
        this IServiceCollection services,
        string connectionString,
        string encryptionKey,
        X509Certificate2 signingKey)
    {
        services.AddDbContextFactory<OpenIdConnectContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseOpenIddict();
        });

        SetUpOpenIdDict(services, encryptionKey, signingKey);

        return services;
    }

    private static void SetUpOpenIdDict(IServiceCollection services, string encryptionKey, X509Certificate2 signingKey)
    {
        services.AddQuartz(options =>
        {
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzHostedService(options =>
            options.WaitForJobsToComplete = true);

        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                  .UseDbContext<OpenIdConnectContext>();
                options.UseQuartz();
            })
            .AddServer(options =>
            {
                options
                    .SetAuthorizationEndpointUris("connect/authorize")
                    .SetLogoutEndpointUris("/Account/Logout")
                    .SetTokenEndpointUris("connect/token")
                    .SetUserinfoEndpointUris("connect/userinfo")
                    .SetIntrospectionEndpointUris("connect/introspect")
                    .SetDeviceEndpointUris("connect/device")
                    .SetVerificationEndpointUris("/Account/Verify");

                options.DisableAccessTokenEncryption();

                options
                    .UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableVerificationEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();

                options.DisableScopeValidation(); //NOTE : delete if need only registered scopes

                options
                    .AllowAuthorizationCodeFlow()
                    .AllowDeviceCodeFlow()
                    .AllowRefreshTokenFlow()
                    .AllowClientCredentialsFlow()
                    .AllowPasswordFlow();

                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String(encryptionKey)));
                options.AddSigningCertificate(signingKey);
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
    }
}
