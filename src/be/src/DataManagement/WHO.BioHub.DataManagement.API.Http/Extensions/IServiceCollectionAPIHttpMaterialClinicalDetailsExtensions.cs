using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialClinicalDetailsExtensions
{
    public static IServiceCollection AddAPIHttpMaterialClinicalDetails(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialClinicalDetailsController, MaterialClinicalDetailsController>();

        return services;
    }
}