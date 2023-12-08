using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialsExtensions
{
    public static IServiceCollection AddAPIHttpMaterials(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialsController, MaterialsController>();

        return services;
    }
}