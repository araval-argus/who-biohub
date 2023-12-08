using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;
using WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionCultivabilityTypesExtensions
{
    public static IServiceCollection AddCoreCultivabilityTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IReadCultivabilityTypeHandler, ReadCultivabilityTypeHandler>()
            .AddScoped<ReadCultivabilityTypeQueryValidator>()

            .AddScoped<IListCultivabilityTypesHandler, ListCultivabilityTypesHandler>()
            .AddScoped<ListCultivabilityTypesQueryValidator>()
            ;

        return services;
    }
}