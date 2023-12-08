using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface ICultivabilityTypePublicReadRepository
{
    Task<CultivabilityType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<CultivabilityType>> List(CancellationToken cancellationToken);
}
