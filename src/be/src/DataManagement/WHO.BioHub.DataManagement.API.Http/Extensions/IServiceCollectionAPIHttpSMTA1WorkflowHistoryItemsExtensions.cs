using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTA1WorkflowHistoryItemsExtensions
{
    public static IServiceCollection AddAPIHttpSMTA1WorkflowHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA1WorkflowHistoryItemsController, SMTA1WorkflowHistoryItemsController>();

        return services;
    }
}

