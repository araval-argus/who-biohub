using Microsoft.Extensions.DependencyInjection;

namespace WHO.BioHub.Search.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSearchExtensions
{
    public static IServiceCollection AddAPIHttpSearch(this IServiceCollection services)
    {
        InjectorExecutor.Inject(services);
        return services;
    }
}