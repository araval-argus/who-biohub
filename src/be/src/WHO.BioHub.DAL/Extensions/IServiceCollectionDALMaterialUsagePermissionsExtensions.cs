using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialUsagePermissionsExtensions
{
    public static IServiceCollection AddDALMaterialUsagePermissions(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialUsagePermissionReadRepository, SQLMaterialUsagePermissionReadRepository>()
            .AddScoped<IMaterialUsagePermissionWriteRepository, SQLMaterialUsagePermissionWriteRepository>();

        return services;
    }
}