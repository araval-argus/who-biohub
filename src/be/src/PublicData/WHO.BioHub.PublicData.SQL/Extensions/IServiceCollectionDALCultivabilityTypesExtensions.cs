using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALCultivabilityTypesExtensions
{
    public static IServiceCollection AddPublicDALCultivabilityTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICultivabilityTypePublicReadRepository, SQLCultivabilityTypePublicReadRepository>();

        return services;
    }
}