using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;
using WHO.BioHub.PublicData.Core.UseCases.TemperatureUnitOfMeasures.ReadTemperatureUnitOfMeasure;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionTemperatureUnitOfMeasuresExtensions
{
    public static IServiceCollection AddCoreTemperatureUnitOfMeasures(this IServiceCollection services)
    {
        services
            .AddScoped<IReadTemperatureUnitOfMeasureHandler, ReadTemperatureUnitOfMeasureHandler>()
            .AddScoped<ReadTemperatureUnitOfMeasureQueryValidator>()

            .AddScoped<IListTemperatureUnitOfMeasuresHandler, ListTemperatureUnitOfMeasuresHandler>()
            .AddScoped<ListTemperatureUnitOfMeasuresQueryValidator>()
            ;

        return services;
    }
}