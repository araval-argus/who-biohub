using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;

public interface ISMTA2WorkflowHistoryItemWriteRepository
{
    Task<Errors?> CopyLinkDocumentFromSMTA2WorkflowItem(Guid SMTA2WorkflowitemId, Guid SMTA2WorkflowhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
    Task<Either<SMTA2WorkflowHistoryItem, Errors>> Create(SMTA2WorkflowHistoryItem SMTA2Workflowhistoryitem, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IDbContextTransaction> GetTransactionAsync();
    Task<SMTA2WorkflowHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(SMTA2WorkflowHistoryItem SMTA2Workflowhistoryitem, CancellationToken cancellationToken);
}
