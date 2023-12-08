using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALIsolationHostTypesExtensions
{
    public static IServiceCollection AddPublicDALIsolationHostTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationHostTypePublicReadRepository, SQLIsolationHostTypePublicReadRepository>();

        return services;
    }
}