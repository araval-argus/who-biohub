using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALIsolationTechniqueTypesExtensions
{
    public static IServiceCollection AddDALIsolationTechniqueTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationTechniqueTypeReadRepository, SQLIsolationTechniqueTypeReadRepository>()
            .AddScoped<IIsolationTechniqueTypeWriteRepository, SQLIsolationTechniqueTypeWriteRepository>();

        return services;
    }
}