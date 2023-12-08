using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.Couriers;

public interface ICourierWriteRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Either<Courier, Errors>> Create(Courier courier, CancellationToken cancellationToken);
    Task<Errors?> CreateCourierHistoryItem(Courier courier, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Courier> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Courier courier, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
