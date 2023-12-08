using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALRolesExtensions
{
    public static IServiceCollection AddDALRoles(this IServiceCollection services)
    {
        services
            .AddScoped<IRoleReadRepository, SQLRoleReadRepository>()
            .AddScoped<IRoleWriteRepository, SQLRoleWriteRepository>();

        return services;
    }
}