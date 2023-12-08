using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpRolesExtensions
{
    public static IServiceCollection AddAPIHttpRoles(this IServiceCollection services)
    {
        services
            .AddScoped<IRolesController, RolesController>();

        return services;
    }
}