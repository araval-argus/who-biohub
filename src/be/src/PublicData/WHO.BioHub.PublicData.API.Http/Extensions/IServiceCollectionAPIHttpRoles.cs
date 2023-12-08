using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpRoles
{
    public static IServiceCollection AddAPIHttpRoles(this IServiceCollection services)
    {
        services
            .AddScoped<IRolesController, RolesController>();

        return services;
    }
}