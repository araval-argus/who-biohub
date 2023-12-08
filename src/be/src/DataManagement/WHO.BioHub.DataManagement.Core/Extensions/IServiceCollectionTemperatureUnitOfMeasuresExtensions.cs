using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.DeleteTemperatureUnitOfMeasure;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ReadTemperatureUnitOfMeasure;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionTemperatureUnitOfMeasuresExtensions
{
    public static IServiceCollection AddCoreTemperatureUnitOfMeasures(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateTemperatureUnitOfMeasureHandler, CreateTemperatureUnitOfMeasureHandler>()
            .AddScoped<ICreateTemperatureUnitOfMeasureMapper, CreateTemperatureUnitOfMeasureMapper>()
            .AddScoped<CreateTemperatureUnitOfMeasureCommandValidator>()

            .AddScoped<IReadTemperatureUnitOfMeasureHandler, ReadTemperatureUnitOfMeasureHandler>()
            .AddScoped<ReadTemperatureUnitOfMeasureQueryValidator>()

            .AddScoped<IUpdateTemperatureUnitOfMeasureHandler, UpdateTemperatureUnitOfMeasureHandler>()
            .AddScoped<IUpdateTemperatureUnitOfMeasureMapper, UpdateTemperatureUnitOfMeasureMapper>()
            .AddScoped<UpdateTemperatureUnitOfMeasureCommandValidator>()

            .AddScoped<IDeleteTemperatureUnitOfMeasureHandler, DeleteTemperatureUnitOfMeasureHandler>()
            .AddScoped<DeleteTemperatureUnitOfMeasureCommandValidator>()

            .AddScoped<IListTemperatureUnitOfMeasuresHandler, ListTemperatureUnitOfMeasuresHandler>()
            .AddScoped<ListTemperatureUnitOfMeasuresQueryValidator>()
            ;

        return services;
    }
}