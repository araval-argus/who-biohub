using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialProducts;

public interface IMaterialProductReadRepository
{
    Task<MaterialProduct> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialProduct>> List(CancellationToken cancellationToken);
}
