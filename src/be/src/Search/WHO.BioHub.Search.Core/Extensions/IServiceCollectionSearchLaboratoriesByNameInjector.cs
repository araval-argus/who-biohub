using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByName;

namespace WHO.BioHub.Search.Core.Extensions;

public class IServiceCollectionSearchLaboratoriesByNameInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        services
            .AddScoped<ISearchLaboratoriesByNameHandler, SearchLaboratoriesByNameHandler>()
            .AddScoped<SearchLaboratoriesByNameQueryValidator>()
            ;

        return services;
    }
}