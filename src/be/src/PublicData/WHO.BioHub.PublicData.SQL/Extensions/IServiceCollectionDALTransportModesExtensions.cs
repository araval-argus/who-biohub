using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALTransportModesExtensions
{
    public static IServiceCollection AddPublicDALTransportModes(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportModePublicReadRepository, SQLTransportModePublicReadRepository>();

        return services;
    }
}