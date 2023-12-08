using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.CultivabilityTypes;

public interface ICultivabilityTypeReadRepository
{
    Task<CultivabilityType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<CultivabilityType>> List(CancellationToken cancellationToken);
}
