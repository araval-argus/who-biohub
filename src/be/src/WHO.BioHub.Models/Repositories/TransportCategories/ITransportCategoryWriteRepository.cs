using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.TransportCategories;

public interface ITransportCategoryWriteRepository
{
    Task<Either<TransportCategory, Errors>> Create(TransportCategory transportcategory, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<TransportCategory> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(TransportCategory transportcategory, CancellationToken cancellationToken);
}
