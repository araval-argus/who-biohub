using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.Core.UseCases.KpiDatas.ReadKpiData;
using WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;
using WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionKpiDatasExtensions
{
    public static IServiceCollection AddCoreKpiDatas(this IServiceCollection services)
    {
        services
            .AddScoped<IReadKpiDataHandler, ReadKpiDataHandler>()
            .AddScoped<ReadKpiDataQueryValidator>()

            ;

        return services;
    }
}