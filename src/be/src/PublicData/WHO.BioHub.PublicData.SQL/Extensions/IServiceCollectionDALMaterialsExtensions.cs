using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Materials;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialsExtensions
{
    public static IServiceCollection AddPublicDALMaterials(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialPublicReadRepository, SQLMaterialPublicReadRepository>();

        return services;
    }
}