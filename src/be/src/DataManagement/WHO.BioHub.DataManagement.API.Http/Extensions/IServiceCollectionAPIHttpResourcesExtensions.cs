using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpResourcesExtensions
{
    public static IServiceCollection AddAPIHttpResources(this IServiceCollection services)
    {
        services
            .AddScoped<IResourcesController, ResourcesController>();

        return services;
    }
}