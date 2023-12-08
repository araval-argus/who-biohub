using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpUserRequestsExtensions
{
    public static IServiceCollection AddAPIHttpUserRequests(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestsController, UserRequestsController>();

        return services;
    }
}