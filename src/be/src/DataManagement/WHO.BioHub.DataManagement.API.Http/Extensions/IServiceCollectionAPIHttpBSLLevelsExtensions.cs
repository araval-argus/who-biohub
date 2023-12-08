using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpBSLLevelsExtensions
{
    public static IServiceCollection AddAPIHttpBSLLevels(this IServiceCollection services)
    {
        services
            .AddScoped<IBSLLevelsController, BSLLevelsController>();

        return services;
    }
}