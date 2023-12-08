using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpKpiDatasExtensions
{
    public static IServiceCollection AddAPIHttpKpiDatas(this IServiceCollection services)
    {
        services
            .AddScoped<IKpiDatasController, KpiDatasController>();

        return services;
    }
}