using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialProductsExtensions
{
    public static IServiceCollection AddAPIHttpMaterialProducts(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialProductsController, MaterialProductsController>();

        return services;
    }
}