using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialProducts;

public interface IMaterialProductWriteRepository
{
    Task<Either<MaterialProduct, Errors>> Create(MaterialProduct materialproduct, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialProduct> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialProduct materialproduct, CancellationToken cancellationToken);
}
