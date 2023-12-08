using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BSLLevels;

public interface IBSLLevelReadRepository
{
    Task<BSLLevel> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BSLLevel>> List(CancellationToken cancellationToken);
}
