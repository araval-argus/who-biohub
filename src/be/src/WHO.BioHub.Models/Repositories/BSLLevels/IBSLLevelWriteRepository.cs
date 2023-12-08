using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BSLLevels;

public interface IBSLLevelWriteRepository
{
    Task<Either<BSLLevel, Errors>> Create(BSLLevel bsllevel, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<BSLLevel> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(BSLLevel bsllevel, CancellationToken cancellationToken);
}
