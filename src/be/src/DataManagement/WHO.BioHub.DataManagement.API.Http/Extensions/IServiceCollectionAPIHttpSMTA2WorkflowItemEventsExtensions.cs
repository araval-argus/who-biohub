using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTA2WorkflowItemEventsExtensions
{
    public static IServiceCollection AddAPIHttpSMTA2WorkflowItemEvents(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA2WorkflowItemEventsController, SMTA2WorkflowItemEventsController>();

        return services;
    }
}
