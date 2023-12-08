using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.API.Http.Extensions;
using WHO.BioHub.Search.Core.Extensions;
using WHO.BioHub.Search.SQL.Extensions;

namespace WHO.BioHub.Search.DependencyInjection;

public class FrontEndGlobalSearchInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpSearch()
                // application
                .AddCoreSearch()
                // Add SQL
                .AddSQLSearchFrontEndGlobalSearch();
    }
}
