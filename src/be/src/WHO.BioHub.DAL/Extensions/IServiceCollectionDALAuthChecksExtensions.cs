using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Models.Repositories.Users;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALAuthChecksExtensions
{
    public static IServiceCollection AddDALAuthChecks(this IServiceCollection services)
    {
        services
           .AddScoped<IUserReadRepository, SQLUserReadRepository>()
           .AddScoped<IUserWriteRepository, SQLUserWriteRepository>()
           .AddScoped<IRoleReadRepository, SQLRoleReadRepository>();

        return services;
    }
}