using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTA2WorkflowItemsExtensions
{
    public static IServiceCollection AddAPIHttpSMTA2WorkflowItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA2WorkflowItemsController, SMTA2WorkflowItemsController>();

        return services;
    }
}