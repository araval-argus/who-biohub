using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItemEvents.ListSMTA1WorkflowItemEvents;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowEventItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTA1WorkflowItemEventsExtensions
{
    public static IServiceCollection AddCoreSMTA1WorkflowItemEvents(this IServiceCollection services)
    {
        services

            .AddScoped<IListSMTA1WorkflowItemEventsHandler, ListSMTA1WorkflowItemEventsHandler>()
            .AddScoped<IListSMTA1WorkflowItemEventMapper, ListSMTA1WorkflowItemEventMapper>()
            .AddScoped<ListSMTA1WorkflowItemEventsQueryValidator>()
            ;

        return services;
    }
}
