using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class WorklistFromBioHubHistoryItemsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpWorklistFromBioHubHistoryItems()
                // application
                .AddCoreWorklistFromBioHubHistoryItems()
                // driven adapters
                .AddDALWorklistFromBioHubHistoryItems();
    }
}
