using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALBSLLevelsExtensions
{
    public static IServiceCollection AddPublicDALBSLLevels(this IServiceCollection services)
    {
        services
            .AddScoped<IBSLLevelPublicReadRepository, SQLBSLLevelPublicReadRepository>();

        return services;
    }
}