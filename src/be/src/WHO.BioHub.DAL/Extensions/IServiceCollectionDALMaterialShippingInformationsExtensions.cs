using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialShippingInformationsExtensions
{
    public static IServiceCollection AddDALMaterialShippingInformations(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialShippingInformationReadRepository, SQLMaterialShippingInformationReadRepository>()
            .AddScoped<IMaterialShippingInformationWriteRepository, SQLMaterialShippingInformationWriteRepository>();

        return services;
    }
}