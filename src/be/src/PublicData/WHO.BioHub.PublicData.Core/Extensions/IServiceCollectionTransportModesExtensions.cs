using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.TransportModes.ListTransportModes;
using WHO.BioHub.PublicData.Core.UseCases.TransportModes.ReadTransportMode;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionTransportModesExtensions
{
    public static IServiceCollection AddCoreTransportModes(this IServiceCollection services)
    {
        services
            .AddScoped<IReadTransportModeHandler, ReadTransportModeHandler>()
            .AddScoped<ReadTransportModeQueryValidator>()

            .AddScoped<IListTransportModesHandler, ListTransportModesHandler>()
            .AddScoped<ListTransportModesQueryValidator>()
            ;

        return services;
    }
}