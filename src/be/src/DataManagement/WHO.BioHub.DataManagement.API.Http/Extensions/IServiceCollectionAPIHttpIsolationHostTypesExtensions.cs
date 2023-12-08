using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpIsolationHostTypesExtensions
{
    public static IServiceCollection AddAPIHttpIsolationHostTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationHostTypesController, IsolationHostTypesController>();

        return services;
    }
}