using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialShippingInformationsHistoryExtensions
{
    public static IServiceCollection AddAPIHttpMaterialShippingInformationsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialShippingInformationsHistoryController, MaterialShippingInformationsHistoryController>();

        return services;
    }
}