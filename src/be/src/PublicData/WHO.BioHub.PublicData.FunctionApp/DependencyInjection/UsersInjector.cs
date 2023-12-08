using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.PublicData.DependencyInjection;

public class UsersInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
          // driven adapters
          .AddPublicDALUsers();
    }
}
