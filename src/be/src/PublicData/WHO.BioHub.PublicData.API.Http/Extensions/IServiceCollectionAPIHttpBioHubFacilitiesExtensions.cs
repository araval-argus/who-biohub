using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpBioHubFacilitiesExtensions
{
    public static IServiceCollection AddAPIHttpBioHubFacilities(this IServiceCollection services)
    {
        services
            .AddScoped<IBioHubFacilitiesController, BioHubFacilitiesController>();

        return services;
    }
}