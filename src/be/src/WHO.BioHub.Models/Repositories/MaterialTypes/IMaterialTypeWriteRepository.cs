using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialTypes;

public interface IMaterialTypeWriteRepository
{
    Task<Either<MaterialType, Errors>> Create(MaterialType materialtype, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialType> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialType materialtype, CancellationToken cancellationToken);
}
