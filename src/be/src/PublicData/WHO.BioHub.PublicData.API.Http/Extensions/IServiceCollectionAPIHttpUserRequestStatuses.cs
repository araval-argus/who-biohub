using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpUserRequestStatuses
{
    public static IServiceCollection AddAPIHttpUserRequestStatuses(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestStatusesController, UserRequestStatusesController>();

        return services;
    }
}