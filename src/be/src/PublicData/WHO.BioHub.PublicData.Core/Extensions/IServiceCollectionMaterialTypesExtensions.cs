using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ListMaterialTypes;
using WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ReadMaterialType;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionMaterialTypesExtensions
{
    public static IServiceCollection AddCoreMaterialTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IReadMaterialTypeHandler, ReadMaterialTypeHandler>()
            .AddScoped<ReadMaterialTypeQueryValidator>()

            .AddScoped<IListMaterialTypesHandler, ListMaterialTypesHandler>()
            .AddScoped<ListMaterialTypesQueryValidator>()
            ;

        return services;
    }
}