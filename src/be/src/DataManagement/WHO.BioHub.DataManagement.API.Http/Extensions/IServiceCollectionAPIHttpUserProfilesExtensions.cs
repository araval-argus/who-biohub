using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpUsersExtensions
{
    public static IServiceCollection AddAPIHttpUsers(this IServiceCollection services)
    {
        services
            .AddScoped<IUsersController, UsersController>();

        return services;
    }
}