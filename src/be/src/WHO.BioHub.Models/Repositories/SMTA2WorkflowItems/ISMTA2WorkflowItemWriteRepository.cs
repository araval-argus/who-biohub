using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;

public interface ISMTA2WorkflowItemWriteRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Either<SMTA2WorkflowItem, Errors>> Create(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Errors?> LinkDocument(Guid SMTA2WorkflowItemId, Guid? documentId, DocumentFileType type, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction transaction = null, bool? replaceExistingType = true);
    Task<SMTA2WorkflowItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> UnlinkDocument(Guid SMTA2WorkflowItemId, Guid? documentId, CancellationToken cancellationToken, bool? isDocumentFile = true, IDbContextTransaction transaction = null);
    Task<Errors?> Update(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
}
