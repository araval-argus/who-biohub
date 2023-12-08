using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.DeleteMaterialUsagePermission;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialUsagePermissionsExtensions
{
    public static IServiceCollection AddCoreMaterialUsagePermissions(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialUsagePermissionHandler, CreateMaterialUsagePermissionHandler>()
            .AddScoped<ICreateMaterialUsagePermissionMapper, CreateMaterialUsagePermissionMapper>()
            .AddScoped<CreateMaterialUsagePermissionCommandValidator>()

            .AddScoped<IReadMaterialUsagePermissionHandler, ReadMaterialUsagePermissionHandler>()
            .AddScoped<ReadMaterialUsagePermissionQueryValidator>()

            .AddScoped<IUpdateMaterialUsagePermissionHandler, UpdateMaterialUsagePermissionHandler>()
            .AddScoped<IUpdateMaterialUsagePermissionMapper, UpdateMaterialUsagePermissionMapper>()
            .AddScoped<UpdateMaterialUsagePermissionCommandValidator>()

            .AddScoped<IDeleteMaterialUsagePermissionHandler, DeleteMaterialUsagePermissionHandler>()
            .AddScoped<DeleteMaterialUsagePermissionCommandValidator>()

            .AddScoped<IListMaterialUsagePermissionsHandler, ListMaterialUsagePermissionsHandler>()
            .AddScoped<ListMaterialUsagePermissionsQueryValidator>()
            ;

        return services;
    }
}