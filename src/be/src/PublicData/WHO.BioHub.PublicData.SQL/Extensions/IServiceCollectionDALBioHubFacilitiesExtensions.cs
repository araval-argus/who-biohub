using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALBioHubFacilitiesExtensions
{
    public static IServiceCollection AddPublicDALBioHubFacilities(this IServiceCollection services)
    {
        services
            .AddScoped<IBioHubFacilityPublicReadRepository, SQLBioHubFacilityPublicReadRepository>();

        return services;
    }
}