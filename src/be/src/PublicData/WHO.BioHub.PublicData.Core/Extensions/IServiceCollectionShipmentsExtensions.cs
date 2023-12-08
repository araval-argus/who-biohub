using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListShipments;
using WHO.BioHub.PublicData.Core.UseCases.Shipments.ListShipments;
using WHO.BioHub.PublicData.Core.UseCases.Shipments.ReadShipment;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionShipmentsExtensions
{
    public static IServiceCollection AddCoreShipments(this IServiceCollection services)
    {
        services
            .AddScoped<IReadShipmentHandler, ReadShipmentHandler>()
            .AddScoped<ReadShipmentQueryValidator>()

            .AddScoped<IListShipmentsHandler, ListShipmentsHandler>()
            .AddScoped<ListShipmentsQueryValidator>()

             .AddScoped<IReadShipmentMapper, ReadShipmentMapper>()
            .AddScoped<IListShipmentMapper, ListShipmentMapper>()

            ;

        return services;
    }
}