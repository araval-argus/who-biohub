using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItemEvents.ListWorklistFromBioHubItemEvents;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubEventItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistFromBioHubItemEventsExtensions
{
    public static IServiceCollection AddCoreWorklistFromBioHubItemEvents(this IServiceCollection services)
    {
        services

            .AddScoped<IListWorklistFromBioHubItemEventsHandler, ListWorklistFromBioHubItemEventsHandler>()
            .AddScoped<IListWorklistFromBioHubItemEventMapper, ListWorklistFromBioHubItemEventMapper>()
            .AddScoped<ListWorklistFromBioHubItemEventsQueryValidator>()
            ;

        return services;
    }
}