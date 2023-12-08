using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.Laboratories;

public interface ILaboratoryWriteRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Either<Laboratory, Errors>> Create(Laboratory laboratory, CancellationToken cancellationToken);
    Task<Errors?> CreateLaboratoryHistoryItem(Laboratory laboratory, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Laboratory> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Laboratory laboratory, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
