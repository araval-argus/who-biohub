using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistToBioHubItemEventsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistToBioHubItemEvents(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubItemEventsController, WorklistToBioHubItemEventsController>();

        return services;
    }
}
