using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALTransportModesExtensions
{
    public static IServiceCollection AddDALTransportModes(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportModeReadRepository, SQLTransportModeReadRepository>()
            .AddScoped<ITransportModeWriteRepository, SQLTransportModeWriteRepository>();

        return services;
    }
}