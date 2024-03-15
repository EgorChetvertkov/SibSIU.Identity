using Microsoft.Extensions.DependencyInjection;

using SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean;
using SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents;
using SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean;

namespace SibSIU.Domain.Dean;
public static class DomainDeanExtensions
{
    public static void AddDomainDeanServices(this IServiceCollection services)
    {
        services.AddScoped<ISynchronizationWithDeanHandler, SynchronizationWithDeanHandler>();
        services.AddScoped<IImportingStudentsHandler, ImportingStudentsHandler>();
        services.AddScoped<ISaveStudentsHandler, SaveStudentsHandler>();
    }
}
