using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.CreateShipment;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.DeleteShipment;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionShipmentsExtensions
{
    public static IServiceCollection AddCoreShipments(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateShipmentHandler, CreateShipmentHandler>()
            .AddScoped<ICreateShipmentMapper, CreateShipmentMapper>()
            .AddScoped<CreateShipmentCommandValidator>()

            .AddScoped<IReadShipmentHandler, ReadShipmentHandler>()
            .AddScoped<IReadShipmentMapper, ReadShipmentMapper>()
            .AddScoped<ReadShipmentQueryValidator>()

            .AddScoped<IUpdateShipmentHandler, UpdateShipmentHandler>()
            .AddScoped<IUpdateShipmentMapper, UpdateShipmentMapper>()
            .AddScoped<UpdateShipmentCommandValidator>()

            .AddScoped<IDeleteShipmentHandler, DeleteShipmentHandler>()
            .AddScoped<DeleteShipmentCommandValidator>()

            .AddScoped<IListShipmentsHandler, ListShipmentsHandler>()
            .AddScoped<IListShipmentsMapper, ListShipmentsMapper>()
            .AddScoped<ListShipmentsQueryValidator>()
            ;

        return services;
    }
}