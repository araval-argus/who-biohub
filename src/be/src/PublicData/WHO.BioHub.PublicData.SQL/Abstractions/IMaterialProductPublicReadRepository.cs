using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IMaterialProductPublicReadRepository
{
    Task<MaterialProduct> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialProduct>> List(CancellationToken cancellationToken);
}
