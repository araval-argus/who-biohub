using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;
using WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionMaterialUsagePermissionsExtensions
{
    public static IServiceCollection AddCoreMaterialUsagePermissions(this IServiceCollection services)
    {
        services
            .AddScoped<IReadMaterialUsagePermissionHandler, ReadMaterialUsagePermissionHandler>()
            .AddScoped<ReadMaterialUsagePermissionQueryValidator>()

            .AddScoped<IListMaterialUsagePermissionsHandler, ListMaterialUsagePermissionsHandler>()
            .AddScoped<ListMaterialUsagePermissionsQueryValidator>()
            ;

        return services;
    }
}