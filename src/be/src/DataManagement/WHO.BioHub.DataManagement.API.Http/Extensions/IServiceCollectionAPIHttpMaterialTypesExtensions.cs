using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialTypesExtensions
{
    public static IServiceCollection AddAPIHttpMaterialTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialTypesController, MaterialTypesController>();

        return services;
    }
}