using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpIsolationHostTypesExtensions
{
    public static IServiceCollection AddAPIHttpIsolationHostTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationHostTypesController, IsolationHostTypesController>();

        return services;
    }
}