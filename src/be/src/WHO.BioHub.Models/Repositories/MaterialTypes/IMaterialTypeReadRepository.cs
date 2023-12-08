using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialTypes;

public interface IMaterialTypeReadRepository
{
    Task<MaterialType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialType>> List(CancellationToken cancellationToken);
}
