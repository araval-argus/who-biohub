using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialProductsExtensions
{
    public static IServiceCollection AddDALMaterialProducts(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialProductReadRepository, SQLMaterialProductReadRepository>()
            .AddScoped<IMaterialProductWriteRepository, SQLMaterialProductWriteRepository>();

        return services;
    }
}