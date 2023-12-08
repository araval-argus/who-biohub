using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WHO.BioHub.Search.FunctionApp;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        return services;
    }
}