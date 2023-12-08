using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialClinicalDetailsExtensions
{
    public static IServiceCollection AddDALMaterialClinicalDetails(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialClinicalDetailReadRepository, SQLMaterialClinicalDetailReadRepository>()
            .AddScoped<IMaterialClinicalDetailWriteRepository, SQLMaterialClinicalDetailWriteRepository>();

        return services;
    }
}