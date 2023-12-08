using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialShippingInformationsHistoryExtensions
{
    public static IServiceCollection AddDALMaterialShippingInformationsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialShippingInformationHistoryReadRepository, SQLMaterialShippingInformationHistoryReadRepository>()
            .AddScoped<IMaterialShippingInformationHistoryWriteRepository, SQLMaterialShippingInformationHistoryWriteRepository>();

        return services;
    }
}