using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IBSLLevelPublicReadRepository
{
    Task<BSLLevel> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BSLLevel>> List(CancellationToken cancellationToken);
}
