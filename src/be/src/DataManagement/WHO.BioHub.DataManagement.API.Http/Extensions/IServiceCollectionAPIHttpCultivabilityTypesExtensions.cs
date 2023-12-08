using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpCultivabilityTypesExtensions
{
    public static IServiceCollection AddAPIHttpCultivabilityTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICultivabilityTypesController, CultivabilityTypesController>();

        return services;
    }
}