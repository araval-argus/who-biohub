using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.Shipments;

public interface IShipmentWriteRepository
{
    Task<Either<Shipment, Errors>> Create(Shipment shipment, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Shipment> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Shipment shipment, CancellationToken cancellationToken);
}
