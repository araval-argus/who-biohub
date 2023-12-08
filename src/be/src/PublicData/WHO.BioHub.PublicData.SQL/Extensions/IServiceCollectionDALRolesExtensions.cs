using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALRolesExtensions
{
    public static IServiceCollection AddPublicDALRoles(this IServiceCollection services)
    {
        services
            .AddScoped<IRolePublicReadRepository, SQLRolePublicReadRepository>();

        return services;
    }
}