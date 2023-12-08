using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IMaterialTypePublicReadRepository
{
    Task<MaterialType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialType>> List(CancellationToken cancellationToken);
}
