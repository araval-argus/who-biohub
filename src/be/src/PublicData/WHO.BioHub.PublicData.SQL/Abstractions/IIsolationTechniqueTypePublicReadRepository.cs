using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IIsolationTechniqueTypePublicReadRepository
{
    Task<IsolationTechniqueType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<IsolationTechniqueType>> List(CancellationToken cancellationToken);
}
