using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpBioHubFacilitiesExtensions
{
    public static IServiceCollection AddAPIHttpBioHubFacilities(this IServiceCollection services)
    {
        services
            .AddScoped<IBioHubFacilitiesController, BioHubFacilitiesController>();

        return services;
    }
}