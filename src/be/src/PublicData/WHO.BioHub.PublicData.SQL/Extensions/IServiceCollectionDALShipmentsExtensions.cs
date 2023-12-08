using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALShipmentsExtensions
{
    public static IServiceCollection AddPublicDALShipments(this IServiceCollection services)
    {
        services
            .AddScoped<IShipmentPublicReadRepository, SQLShipmentPublicReadRepository>();

        return services;
    }
}