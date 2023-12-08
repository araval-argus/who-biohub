using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.Roles.ListRoles;
using WHO.BioHub.PublicData.Core.UseCases.Roles.ReadRole;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionRolesExtensions
{
    public static IServiceCollection AddCoreRoles(this IServiceCollection services)
    {
        services
            .AddScoped<IReadRoleHandler, ReadRoleHandler>()
            .AddScoped<ReadRoleQueryValidator>()

            .AddScoped<IListRolesHandler, ListRolesHandler>()
            .AddScoped<ListRolesQueryValidator>()
            ;

        return services;
    }
}