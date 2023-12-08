using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTA1WorkflowItemEventsExtensions
{
    public static IServiceCollection AddAPIHttpSMTA1WorkflowItemEvents(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA1WorkflowItemEventsController, SMTA1WorkflowItemEventsController>();

        return services;
    }
}