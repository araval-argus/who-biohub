using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class WorklistFromBioHubEmailsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driven adapters
                .AddDALWorklistFromBioHubEmails();
    }
}
