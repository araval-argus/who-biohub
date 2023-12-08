using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpShipmentsExtensions
{
    public static IServiceCollection AddAPIHttpShipments(this IServiceCollection services)
    {
        services
            .AddScoped<IShipmentsController, ShipmentsController>();

        return services;
    }
}