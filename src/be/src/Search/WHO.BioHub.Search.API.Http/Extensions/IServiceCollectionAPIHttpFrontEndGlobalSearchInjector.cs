using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.API.Http.Controllers.FrontEndGlobalSearch;

namespace WHO.BioHub.Search.API.Http.Extensions;

public class IServiceCollectionAPIHttpFrontEndGlobalSearchInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        services
            .AddScoped<IFrontEndGlobalSearchController, FrontEndGlobalSearchController>();

        return services;
    }
}