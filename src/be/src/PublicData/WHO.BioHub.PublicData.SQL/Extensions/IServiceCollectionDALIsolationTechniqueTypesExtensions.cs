using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALIsolationTechniqueTypesExtensions
{
    public static IServiceCollection AddPublicDALIsolationTechniqueTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationTechniqueTypePublicReadRepository, SQLIsolationTechniqueTypePublicReadRepository>();

        return services;
    }
}