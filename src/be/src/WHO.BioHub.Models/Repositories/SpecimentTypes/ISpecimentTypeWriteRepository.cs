using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.SpecimenTypes;

public interface ISpecimenTypeWriteRepository
{
    Task<Either<SpecimenType, Errors>> Create(SpecimenType specimenttype, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<SpecimenType> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(SpecimenType specimenttype, CancellationToken cancellationToken);
}
