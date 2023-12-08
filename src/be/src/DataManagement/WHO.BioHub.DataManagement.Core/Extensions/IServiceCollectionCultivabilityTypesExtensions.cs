using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.DeleteCultivabilityType;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionCultivabilityTypesExtensions
{
    public static IServiceCollection AddCoreCultivabilityTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCultivabilityTypeHandler, CreateCultivabilityTypeHandler>()
            .AddScoped<ICreateCultivabilityTypeMapper, CreateCultivabilityTypeMapper>()
            .AddScoped<CreateCultivabilityTypeCommandValidator>()

            .AddScoped<IReadCultivabilityTypeHandler, ReadCultivabilityTypeHandler>()
            .AddScoped<ReadCultivabilityTypeQueryValidator>()

            .AddScoped<IUpdateCultivabilityTypeHandler, UpdateCultivabilityTypeHandler>()
            .AddScoped<IUpdateCultivabilityTypeMapper, UpdateCultivabilityTypeMapper>()
            .AddScoped<UpdateCultivabilityTypeCommandValidator>()

            .AddScoped<IDeleteCultivabilityTypeHandler, DeleteCultivabilityTypeHandler>()
            .AddScoped<DeleteCultivabilityTypeCommandValidator>()

            .AddScoped<IListCultivabilityTypesHandler, ListCultivabilityTypesHandler>()
            .AddScoped<ListCultivabilityTypesQueryValidator>()
            ;

        return services;
    }
}