using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialProductsExtensions
{
    public static IServiceCollection AddPublicDALMaterialProducts(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialProductPublicReadRepository, SQLMaterialProductPublicReadRepository>();

        return services;
    }
}