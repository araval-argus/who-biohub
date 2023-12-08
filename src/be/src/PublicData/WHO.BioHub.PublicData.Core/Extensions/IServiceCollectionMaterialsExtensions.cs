using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;
using WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionMaterialsExtensions
{
    public static IServiceCollection AddCoreMaterials(this IServiceCollection services)
    {
        services
            .AddScoped<IReadMaterialHandler, ReadMaterialHandler>()
            .AddScoped<ReadMaterialQueryValidator>()

            .AddScoped<IListMaterialsHandler, ListMaterialsHandler>()
            .AddScoped<ListMaterialsQueryValidator>()
            .AddScoped<IReadMaterialMapper, ReadMaterialMapper>()
            .AddScoped<IListMaterialMapper, ListMaterialMapper>()

            ;

        return services;
    }
}