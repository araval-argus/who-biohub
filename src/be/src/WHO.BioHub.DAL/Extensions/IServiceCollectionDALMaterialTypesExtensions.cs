using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialTypesExtensions
{
    public static IServiceCollection AddDALMaterialTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialTypeReadRepository, SQLMaterialTypeReadRepository>()
            .AddScoped<IMaterialTypeWriteRepository, SQLMaterialTypeWriteRepository>();

        return services;
    }
}