using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTA2WorkflowHistoryItemsExtensions
{
    public static IServiceCollection AddAPIHttpSMTA2WorkflowHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA2WorkflowHistoryItemsController, SMTA2WorkflowHistoryItemsController>();

        return services;
    }
}