using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByCountry;

namespace WHO.BioHub.Search.Core.Extensions;

public class IServiceCollectionSearchLaboratoriesByCountryInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        services
            .AddScoped<ISearchLaboratoriesByCountryHandler, SearchLaboratoriesByCountryHandler>()
            .AddScoped<SearchLaboratoriesByCountryQueryValidator>()
            ;

        return services;
    }
}