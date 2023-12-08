using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItemEvents.ListSMTA2WorkflowItemEvents;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowEventItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTA2WorkflowItemEventsExtensions
{
    public static IServiceCollection AddCoreSMTA2WorkflowItemEvents(this IServiceCollection services)
    {
        services

            .AddScoped<IListSMTA2WorkflowItemEventsHandler, ListSMTA2WorkflowItemEventsHandler>()
            .AddScoped<IListSMTA2WorkflowItemEventMapper, ListSMTA2WorkflowItemEventMapper>()
            .AddScoped<ListSMTA2WorkflowItemEventsQueryValidator>()
            ;

        return services;
    }
}
