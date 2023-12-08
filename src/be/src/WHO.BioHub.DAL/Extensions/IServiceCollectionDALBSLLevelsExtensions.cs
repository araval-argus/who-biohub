using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALBSLLevelsExtensions
{
    public static IServiceCollection AddDALBSLLevels(this IServiceCollection services)
    {
        services
            .AddScoped<IBSLLevelReadRepository, SQLBSLLevelReadRepository>()
            .AddScoped<IBSLLevelWriteRepository, SQLBSLLevelWriteRepository>();

        return services;
    }
}