using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALResourcesExtensions
{
    public static IServiceCollection AddPublicDALResources(this IServiceCollection services)
    {
        services
            .AddScoped<IResourcePublicReadRepository, SQLResourcePublicReadRepository>();

        return services;
    }
}