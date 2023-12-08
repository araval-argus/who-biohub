using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Extensions;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class SMTA1WorkflowItemsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpSMTA1WorkflowItems()
                // application
                .AddCoreSMTA1WorkflowItems()
                // driven adapters
                .AddDALSMTA1WorkflowItems();
    }
}
