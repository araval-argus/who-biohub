using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class SMTARequestsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpSMTARequests()
                // application
                .AddCoreSMTARequests();

    }
}
