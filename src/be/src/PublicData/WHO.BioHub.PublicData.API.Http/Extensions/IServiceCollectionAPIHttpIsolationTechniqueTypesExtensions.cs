using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpIsolationTechniqueTypesExtensions
{
    public static IServiceCollection AddAPIHttpIsolationTechniqueTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationTechniqueTypesController, IsolationTechniqueTypesController>();

        return services;
    }
}