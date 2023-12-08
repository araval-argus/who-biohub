using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialUsagePermissionsExtensions
{
    public static IServiceCollection AddPublicDALMaterialUsagePermissions(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialUsagePermissionPublicReadRepository, SQLMaterialUsagePermissionPublicReadRepository>();

        return services;
    }
}