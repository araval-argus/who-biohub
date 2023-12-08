using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALMaterialTypesExtensions
{
    public static IServiceCollection AddPublicDALMaterialTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IMaterialTypePublicReadRepository, SQLMaterialTypePublicReadRepository>();

        return services;
    }
}