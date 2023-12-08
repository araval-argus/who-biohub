using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSMTARequestsExtensions
{
    public static IServiceCollection AddAPIHttpSMTARequests(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTARequestsController, SMTARequestsController>();

        return services;
    }
}