using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpTransportCategoriesExtensions
{
    public static IServiceCollection AddAPIHttpTransportCategories(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportCategoriesController, TransportCategoriesController>();

        return services;
    }
}