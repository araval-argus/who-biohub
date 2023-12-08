using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber
{
    public interface IWorklistItemUsedReferenceNumberWriteRepository
    {
        Task<Either<Models.WorklistItemUsedReferenceNumber, Errors>> Create(Models.WorklistItemUsedReferenceNumber worklistItemUsedReferenceNumber, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task<Either<Models.WorklistItemUsedReferenceNumber, Errors>> Create(bool? isPast, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    }
}


