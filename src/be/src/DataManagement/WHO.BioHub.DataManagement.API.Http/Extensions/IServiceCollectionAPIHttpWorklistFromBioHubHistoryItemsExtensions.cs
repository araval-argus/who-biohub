using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistFromBioHubHistoryItemsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistFromBioHubHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistFromBioHubHistoryItemsController, WorklistFromBioHubHistoryItemsController>();

        return services;
    }
}