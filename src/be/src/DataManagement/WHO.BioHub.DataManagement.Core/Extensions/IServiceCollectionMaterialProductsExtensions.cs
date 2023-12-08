using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.DeleteMaterialProduct;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ListMaterialProducts;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ReadMaterialProduct;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialProductsExtensions
{
    public static IServiceCollection AddCoreMaterialProducts(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialProductHandler, CreateMaterialProductHandler>()
            .AddScoped<ICreateMaterialProductMapper, CreateMaterialProductMapper>()
            .AddScoped<CreateMaterialProductCommandValidator>()

            .AddScoped<IReadMaterialProductHandler, ReadMaterialProductHandler>()
            .AddScoped<ReadMaterialProductQueryValidator>()

            .AddScoped<IUpdateMaterialProductHandler, UpdateMaterialProductHandler>()
            .AddScoped<IUpdateMaterialProductMapper, UpdateMaterialProductMapper>()
            .AddScoped<UpdateMaterialProductCommandValidator>()

            .AddScoped<IDeleteMaterialProductHandler, DeleteMaterialProductHandler>()
            .AddScoped<DeleteMaterialProductCommandValidator>()

            .AddScoped<IListMaterialProductsHandler, ListMaterialProductsHandler>()
            .AddScoped<ListMaterialProductsQueryValidator>()
            ;

        return services;
    }
}