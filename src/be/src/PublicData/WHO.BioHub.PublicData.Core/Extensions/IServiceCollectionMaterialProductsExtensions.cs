using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ListMaterialProducts;
using WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ReadMaterialProduct;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionMaterialProductsExtensions
{
    public static IServiceCollection AddCoreMaterialProducts(this IServiceCollection services)
    {
        services
            .AddScoped<IReadMaterialProductHandler, ReadMaterialProductHandler>()
            .AddScoped<ReadMaterialProductQueryValidator>()

            .AddScoped<IListMaterialProductsHandler, ListMaterialProductsHandler>()
            .AddScoped<ListMaterialProductsQueryValidator>()
            ;

        return services;
    }
}