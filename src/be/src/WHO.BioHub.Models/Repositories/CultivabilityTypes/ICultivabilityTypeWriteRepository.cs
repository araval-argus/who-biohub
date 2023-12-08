using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.CultivabilityTypes;

public interface ICultivabilityTypeWriteRepository
{
    Task<Either<CultivabilityType, Errors>> Create(CultivabilityType cultivabilitytype, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<CultivabilityType> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(CultivabilityType cultivabilitytype, CancellationToken cancellationToken);
}
