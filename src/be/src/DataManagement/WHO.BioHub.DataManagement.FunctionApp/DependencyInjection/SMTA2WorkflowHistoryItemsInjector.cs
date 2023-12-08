using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class SMTA2WorkflowHistoryItemsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpSMTA2WorkflowHistoryItems()
                // application
                .AddCoreSMTA2WorkflowHistoryItems()
                // driven adapters
                .AddDALSMTA2WorkflowHistoryItems();
    }
}
