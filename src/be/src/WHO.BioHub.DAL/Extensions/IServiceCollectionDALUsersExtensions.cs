using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALUsersExtensions
{
    public static IServiceCollection AddDALUsers(this IServiceCollection services)
    {
        services
            .AddScoped<IUserReadRepository, SQLUserReadRepository>()
            .AddScoped<IUserWriteRepository, SQLUserWriteRepository>();

        return services;
    }
}