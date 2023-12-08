using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpAuthChecksExtensions
{
    public static IServiceCollection AddAPIHttpAuthChecks(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthChecksController, AuthChecksController>();

        return services;
    }
}