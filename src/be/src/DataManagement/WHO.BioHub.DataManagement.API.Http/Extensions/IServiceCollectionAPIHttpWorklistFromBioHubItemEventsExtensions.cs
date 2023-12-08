using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistFromBioHubItemEventsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistFromBioHubItemEvents(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistFromBioHubItemEventsController, WorklistFromBioHubItemEventsController>();

        return services;
    }
}
