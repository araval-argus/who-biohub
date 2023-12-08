using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.Core.UseCases.Aggregates.FrontEndGlobalSearch;

namespace WHO.BioHub.Search.Core.Extensions;

public class IServiceCollectionFrontEndGlobalSearchInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        services
            .AddScoped<IFrontEndGlobalSearchHandler, FrontEndGlobalSearchHandler>()
            .AddScoped<FrontEndGlobalSearchQueryValidator>()
            ;

        return services;
    }
}