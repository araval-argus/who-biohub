using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Public.SQL.Repositories;
using WHO.BioHub.PublicData.SQL.Abstractions;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALUsersExtensions
{
    public static IServiceCollection AddPublicDALUsers(this IServiceCollection services)
    {
        services
         .AddScoped<IUserPublicReadRepository, SQLUserPublicReadRepository>();

        return services;
    }
}


