using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Extensions;
using WHO.BioHub.PublicData.Core.Extensions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.PublicData.DependencyInjection;

public class GeneticSequenceDatasInjector : IInjector
{
    public IServiceCollection InjectDependencies(IServiceCollection services)
    {
        return services
                // driving adapters
                .AddAPIHttpGeneticSequenceDatas()
                // application
                .AddCoreGeneticSequenceDatas()
                // driven adapters
                .AddPublicDALGeneticSequenceDatas();
    }
}
