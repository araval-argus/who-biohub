using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALBioHubFacilitiesExtensions
{
    public static IServiceCollection AddDALBioHubFacilities(this IServiceCollection services)
    {
        services
            .AddScoped<IBioHubFacilityReadRepository, SQLBioHubFacilityReadRepository>()
            .AddScoped<IBioHubFacilityWriteRepository, SQLBioHubFacilityWriteRepository>();

        return services;
    }
}