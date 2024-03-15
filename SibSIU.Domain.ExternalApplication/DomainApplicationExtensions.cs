using Microsoft.Extensions.DependencyInjection;

using SibSIU.Domain.ExternalApplication.Applications.Commands.Create;
using SibSIU.Domain.ExternalApplication.Applications.Commands.Delete;
using SibSIU.Domain.ExternalApplication.Applications.Commands.Update;
using SibSIU.Domain.ExternalApplication.Applications.Queries.GetDetails;
using SibSIU.Domain.ExternalApplication.Applications.Queries.GetPage;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.Create;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.Delete;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.DeleteByCreator;
using SibSIU.Domain.ExternalApplication.CORSes.Commands.Update;
using SibSIU.Domain.ExternalApplication.CORSes.Queries.GetDetails;
using SibSIU.Domain.ExternalApplication.CORSes.Queries.GetPage;
using SibSIU.Domain.ExternalApplication.CORSes.Queries.GetSelectList;
using SibSIU.Domain.ExternalApplication.CORSes.Validations;

namespace SibSIU.Domain.ExternalApplication;
public static class DomainApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateApplicationHandler, CreateApplicationHandler>();
        services.AddScoped<IUpdateApplicationHandler, UpdateApplicationHandler>();
        services.AddScoped<IDeleteApplicationHandler, DeleteApplicationHandler>();
        services.AddScoped<IGetApplicationDetailsHandler, GetApplicationDetailsHandler>();
        services.AddScoped<IGetApplicationPageHandler, GetApplicationPageHandler>();

        services.AddScoped<ICreateCORSHandler, CreateCORSHandler>();
        services.AddScoped<IUpdateCORSHandler, UpdateCORSHandler>();
        services.AddScoped<IDeleteCORSHandler, DeleteCORSHandler>();
        services.AddScoped<IDeleteCORSByCreatorHandler, DeleteCORSByCreatorHandler>();
        services.AddScoped<IGetCORSDetailsHandler, GetCORSDetailsHandler>();
        services.AddScoped<IGetCORSPageHandler, GetCORSPageHandler>();
        services.AddScoped<IGetCORSSelectListHandler, GetCORSSelectListHandler>();

        services.AddTransient<CORSMiddleware>();
    }
}
