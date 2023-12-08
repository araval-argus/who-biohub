using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpTransportModesExtensions
{
    public static IServiceCollection AddAPIHttpTransportModes(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportModesController, TransportModesController>();

        return services;
    }
}