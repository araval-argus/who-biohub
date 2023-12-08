using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.DependencyInjection;

public class EFormsInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpEForms()
                // application
                .AddCoreEForms();
               
    }
}
