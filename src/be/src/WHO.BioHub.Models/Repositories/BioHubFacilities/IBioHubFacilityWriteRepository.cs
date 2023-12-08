using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.BioHubFacilities;

public interface IBioHubFacilityWriteRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Either<BioHubFacility, Errors>> Create(BioHubFacility biohubfacility, CancellationToken cancellationToken);
    Task<Errors?> CreateBioHubFacilityHistoryItem(BioHubFacility bioHubFacility, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<BioHubFacility> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(BioHubFacility biohubfacility, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
