using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialShippingInformationsExtensions
{
    public static IServiceCollection AddAPIHttpMaterialShippingInformations(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialShippingInformationsController, MaterialShippingInformationsController>();

        return services;
    }
}