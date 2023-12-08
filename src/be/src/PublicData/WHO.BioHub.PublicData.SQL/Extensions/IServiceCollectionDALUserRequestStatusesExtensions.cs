using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALUserRequestStatusesExtensions
{
    public static IServiceCollection AddPublicDALUserRequestStatuses(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestStatusPublicReadRepository, SQLUserRequestStatusPublicReadRepository>();

        return services;
    }
}