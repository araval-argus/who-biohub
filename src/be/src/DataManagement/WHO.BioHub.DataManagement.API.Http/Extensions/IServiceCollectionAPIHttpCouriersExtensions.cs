using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpCouriersExtensions
{
    public static IServiceCollection AddAPIHttpCouriers(this IServiceCollection services)
    {
        services
            .AddScoped<ICouriersController, CouriersController>();

        return services;
    }
}