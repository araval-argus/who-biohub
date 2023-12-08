using Microsoft.Extensions.DependencyInjection;

namespace WHO.BioHub.Search.Core.Extensions;

public static class IServiceCollectionLaboratoriesExtensions
{
    public static IServiceCollection AddCoreSearch(this IServiceCollection services)
    {
        InjectorExecutor.Inject(services);
        return services;
    }
}