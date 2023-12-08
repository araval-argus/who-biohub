using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.CreateRole;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.DeleteRole;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.ListRoles;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.ReadRole;
using WHO.BioHub.DataManagement.Core.UseCases.Roles.UpdateRole;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionRolesExtensions
{
    public static IServiceCollection AddCoreRoles(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateRoleHandler, CreateRoleHandler>()
            .AddScoped<ICreateRoleMapper, CreateRoleMapper>()
            .AddScoped<CreateRoleCommandValidator>()

            .AddScoped<IReadRoleHandler, ReadRoleHandler>()
            .AddScoped<ReadRoleQueryValidator>()

            .AddScoped<IUpdateRoleHandler, UpdateRoleHandler>()
            .AddScoped<IUpdateRoleMapper, UpdateRoleMapper>()
            .AddScoped<UpdateRoleCommandValidator>()

            .AddScoped<IDeleteRoleHandler, DeleteRoleHandler>()
            .AddScoped<DeleteRoleCommandValidator>()

            .AddScoped<IListRolesHandler, ListRolesHandler>()
            .AddScoped<ListRolesQueryValidator>()
            ;

        return services;
    }
}