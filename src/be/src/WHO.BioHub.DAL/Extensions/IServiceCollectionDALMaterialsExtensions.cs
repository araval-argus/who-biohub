using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialsExtensions
{
    public static IServiceCollection AddDALMaterials(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialReadRepository, SQLMaterialReadRepository>()
            .AddScoped<IMaterialWriteRepository, SQLMaterialWriteRepository>();

        return services;
    }
}