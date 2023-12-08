using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALShipmentsExtensions
{
    public static IServiceCollection AddDALShipments(this IServiceCollection services)
    {
        services
            .AddScoped<IShipmentReadRepository, SQLShipmentReadRepository>()
            .AddScoped<IShipmentWriteRepository, SQLShipmentWriteRepository>();

        return services;
    }
}