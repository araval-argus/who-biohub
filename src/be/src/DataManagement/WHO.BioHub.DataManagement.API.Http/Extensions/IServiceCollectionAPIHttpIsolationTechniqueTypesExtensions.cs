using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpIsolationTechniqueTypesExtensions
{
    public static IServiceCollection AddAPIHttpIsolationTechniqueTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationTechniqueTypesController, IsolationTechniqueTypesController>();

        return services;
    }
}