using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTA1WorkflowItemsExtensions
{
    public static IServiceCollection AddAPIHttpSMTA1WorkflowItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA1WorkflowItemsController, SMTA1WorkflowItemsController>();

        return services;
    }
}

