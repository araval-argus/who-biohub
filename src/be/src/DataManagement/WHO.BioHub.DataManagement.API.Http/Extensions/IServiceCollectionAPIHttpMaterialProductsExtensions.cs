using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialProductsExtensions
{
    public static IServiceCollection AddAPIHttpMaterialProducts(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialProductsController, MaterialProductsController>();

        return services;
    }
}