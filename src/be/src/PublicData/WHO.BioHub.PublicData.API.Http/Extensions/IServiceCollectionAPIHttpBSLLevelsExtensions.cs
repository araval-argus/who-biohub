using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpBSLLevelsExtensions
{
    public static IServiceCollection AddAPIHttpBSLLevels(this IServiceCollection services)
    {
        services
            .AddScoped<IBSLLevelsController, BSLLevelsController>();

        return services;
    }
}