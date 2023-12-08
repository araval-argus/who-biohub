using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistToBioHubItemsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistToBioHubItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubItemsController, WorklistToBioHubItemsController>();

        return services;
    }
}