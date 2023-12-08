using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionShipmentRequestsExtensions
{
    public static IServiceCollection AddCoreShipmentRequests(this IServiceCollection services)
    {
        services

            .AddScoped<IListShipmentRequestsHandler, ListShipmentRequestsHandler>()
            .AddScoped<IListShipmentRequestsMapper, ListShipmentRequestsMapper>()
            .AddScoped<ListShipmentRequestsQueryValidator>()
            ;

        return services;
    }
}