using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistToBioHubHistoryItemsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistToBioHubHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubHistoryItemsController, WorklistToBioHubHistoryItemsController>();

        return services;
    }
}