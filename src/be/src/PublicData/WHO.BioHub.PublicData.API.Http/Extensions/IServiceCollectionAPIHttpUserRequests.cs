using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpUserRequests
{
    public static IServiceCollection AddAPIHttpUserRequests(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestsController, UserRequestsController>();

        return services;
    }
}