using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialUsagePermissionsExtensions
{
    public static IServiceCollection AddAPIHttpMaterialUsagePermissions(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialUsagePermissionsController, MaterialUsagePermissionsController>();

        return services;
    }
}