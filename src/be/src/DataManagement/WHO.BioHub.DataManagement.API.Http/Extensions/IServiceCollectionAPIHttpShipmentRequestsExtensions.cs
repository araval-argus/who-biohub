using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpShipmentRequestsExtensions
{
    public static IServiceCollection AddAPIHttpShipmentRequests(this IServiceCollection services)
    {
        services
            .AddScoped<IShipmentRequestsController, ShipmentRequestsController>();

        return services;
    }
}