using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialUsagePermissionsExtensions
{
    public static IServiceCollection AddAPIHttpMaterialUsagePermissions(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialUsagePermissionsController, MaterialUsagePermissionsController>();

        return services;
    }
}