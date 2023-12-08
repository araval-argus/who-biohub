using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialTypesExtensions
{
    public static IServiceCollection AddAPIHttpMaterialTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialTypesController, MaterialTypesController>();

        return services;
    }
}