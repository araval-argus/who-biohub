using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALTemperatureUnitOfMeasuresExtensions
{
    public static IServiceCollection AddPublicDALTemperatureUnitOfMeasures(this IServiceCollection services)
    {
        services
            .AddScoped<ITemperatureUnitOfMeasurePublicReadRepository, SQLTemperatureUnitOfMeasurePublicReadRepository>();

        return services;
    }
}