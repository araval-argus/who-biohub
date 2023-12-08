using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItemEvents.ListWorklistToBioHubItemEvents;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubEventItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistToBioHubItemEventsExtensions
{
    public static IServiceCollection AddCoreWorklistToBioHubItemEvents(this IServiceCollection services)
    {
        services

            .AddScoped<IListWorklistToBioHubItemEventsHandler, ListWorklistToBioHubItemEventsHandler>()
            .AddScoped<IListWorklistToBioHubItemEventMapper, ListWorklistToBioHubItemEventMapper>()
            .AddScoped<ListWorklistToBioHubItemEventsQueryValidator>()
            ;

        return services;
    }
}
