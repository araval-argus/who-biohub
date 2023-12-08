using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialClinicalDetailsHistoryExtensions
{
    public static IServiceCollection AddDALMaterialClinicalDetailsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialClinicalDetailHistoryReadRepository, SQLMaterialClinicalDetailHistoryReadRepository>()
            .AddScoped<IMaterialClinicalDetailHistoryWriteRepository, SQLMaterialClinicalDetailHistoryWriteRepository>();

        return services;
    }
}