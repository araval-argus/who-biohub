using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistFromBioHubItemsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistFromBioHubItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistFromBioHubItemsController, WorklistFromBioHubItemsController>();

        return services;
    }
}