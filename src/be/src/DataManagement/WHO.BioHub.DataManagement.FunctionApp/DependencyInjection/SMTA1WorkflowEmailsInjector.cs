using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class SMTA1WorkflowEmailsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                //.AddAPIHttpDocumentTemplates()
                // application
                //.AddCoreDocumentTemplates()
                // driven adapters
                .AddDALSMTA1WorkflowEmails();
    }
}
