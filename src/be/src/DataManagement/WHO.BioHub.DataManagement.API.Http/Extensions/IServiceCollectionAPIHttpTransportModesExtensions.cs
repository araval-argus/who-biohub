using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpTransportModesExtensions
{
    public static IServiceCollection AddAPIHttpTransportModes(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportModesController, TransportModesController>();

        return services;
    }
}