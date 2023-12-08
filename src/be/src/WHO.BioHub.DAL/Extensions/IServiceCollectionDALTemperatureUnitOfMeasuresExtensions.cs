using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALTemperatureUnitOfMeasuresExtensions
{
    public static IServiceCollection AddDALTemperatureUnitOfMeasures(this IServiceCollection services)
    {
        services
            .AddScoped<ITemperatureUnitOfMeasureReadRepository, SQLTemperatureUnitOfMeasureReadRepository>()
            .AddScoped<ITemperatureUnitOfMeasureWriteRepository, SQLTemperatureUnitOfMeasureWriteRepository>();

        return services;
    }
}