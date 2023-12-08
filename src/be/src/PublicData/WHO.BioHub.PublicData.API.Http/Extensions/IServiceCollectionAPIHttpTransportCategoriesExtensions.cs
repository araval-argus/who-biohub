using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpTransportCategoriesExtensions
{
    public static IServiceCollection AddAPIHttpTransportCategories(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportCategoriesController, TransportCategoriesController>();

        return services;
    }
}