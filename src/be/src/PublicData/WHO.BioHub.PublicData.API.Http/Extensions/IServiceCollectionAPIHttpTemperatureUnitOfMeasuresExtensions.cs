using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpTemperatureUnitOfMeasuresExtensions
{
    public static IServiceCollection AddAPIHttpTemperatureUnitOfMeasures(this IServiceCollection services)
    {
        services
            .AddScoped<ITemperatureUnitOfMeasuresController, TemperatureUnitOfMeasuresController>();

        return services;
    }
}