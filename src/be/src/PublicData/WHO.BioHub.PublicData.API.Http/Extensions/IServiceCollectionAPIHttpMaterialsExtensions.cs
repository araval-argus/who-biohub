using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialsExtensions
{
    public static IServiceCollection AddAPIHttpMaterials(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialsController, MaterialsController>();

        return services;
    }
}
