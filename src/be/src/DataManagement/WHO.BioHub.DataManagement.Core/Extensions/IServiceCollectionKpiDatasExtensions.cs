using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Data.Core.UseCases.KpiDatas.ReadKpiData;

namespace WHO.BioHub.DataManagement.Core.Extensions;

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