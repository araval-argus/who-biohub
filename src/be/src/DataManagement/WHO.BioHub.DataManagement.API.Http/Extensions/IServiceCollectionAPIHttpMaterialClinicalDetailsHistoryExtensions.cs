using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpMaterialClinicalDetailsHistoryExtensions
{
    public static IServiceCollection AddAPIHttpMaterialClinicalDetailsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialClinicalDetailsHistoryController, MaterialClinicalDetailsHistoryController>();

        return services;
    }
}