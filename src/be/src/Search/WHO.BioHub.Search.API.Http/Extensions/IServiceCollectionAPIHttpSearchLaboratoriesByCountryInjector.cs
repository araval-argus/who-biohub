using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.API.Http.Controllers.SearchLaboratoriesByCountry;

namespace WHO.BioHub.Search.API.Http.Extensions;

public class IServiceCollectionAPIHttpSearchLaboratoriesByCountryInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        services
            .AddScoped<ISearchLaboratoriesByCountryController, SearchLaboratoriesByCountryController>();

        return services;
    }
}