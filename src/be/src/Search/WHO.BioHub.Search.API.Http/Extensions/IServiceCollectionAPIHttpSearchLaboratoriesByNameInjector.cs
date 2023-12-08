using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.API.Http.Controllers.SearchLaboratoriesByName;

namespace WHO.BioHub.Search.API.Http.Extensions;

public class IServiceCollectionAPIHttpSearchLaboratoriesByNameInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        services
            .AddScoped<ISearchLaboratoriesByNameController, SearchLaboratoriesByNameController>();

        return services;
    }
}