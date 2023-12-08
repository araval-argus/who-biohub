using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class SMTA2WorkflowItemEventsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpSMTA2WorkflowItemEvents()
                // application
                .AddCoreSMTA2WorkflowItemEvents();
    }
}
