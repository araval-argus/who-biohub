using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpUserRequestStatusesExtensions
{
    public static IServiceCollection AddAPIHttpUserRequestStatuses(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestStatusesController, UserRequestStatusesController>();

        return services;
    }
}