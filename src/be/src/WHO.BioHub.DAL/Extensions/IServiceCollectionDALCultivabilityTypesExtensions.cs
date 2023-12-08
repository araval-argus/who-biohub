using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALCultivabilityTypesExtensions
{
    public static IServiceCollection AddDALCultivabilityTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICultivabilityTypeReadRepository, SQLCultivabilityTypeReadRepository>()
            .AddScoped<ICultivabilityTypeWriteRepository, SQLCultivabilityTypeWriteRepository>();

        return services;
    }
}