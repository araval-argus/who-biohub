using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpPriorityRequestTypesExtensions
{
    public static IServiceCollection AddAPIHttpPriorityRequestTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IPriorityRequestTypesController, PriorityRequestTypesController>();

        return services;
    }
}