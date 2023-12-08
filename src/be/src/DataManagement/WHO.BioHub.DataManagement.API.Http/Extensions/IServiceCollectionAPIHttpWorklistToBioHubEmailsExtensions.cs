using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpWorklistToBioHubEmailsExtensions
{
    public static IServiceCollection AddAPIHttpWorklistToBioHubEmails(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubEmailsController, WorklistToBioHubEmailsController>();

        return services;
    }
}