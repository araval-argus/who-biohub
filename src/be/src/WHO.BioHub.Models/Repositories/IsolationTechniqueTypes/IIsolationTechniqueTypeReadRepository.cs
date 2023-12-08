using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;

public interface IIsolationTechniqueTypeReadRepository
{
    Task<IsolationTechniqueType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<IsolationTechniqueType>> List(CancellationToken cancellationToken);
}
