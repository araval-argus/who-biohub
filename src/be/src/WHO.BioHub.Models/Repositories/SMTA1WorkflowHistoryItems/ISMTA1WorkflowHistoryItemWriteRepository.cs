using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems
{
    public interface ISMTA1WorkflowHistoryItemWriteRepository
    {
        Task<Errors?> CopyLinkDocumentFromSMTA1WorkflowItem(Guid SMTA1WorkflowitemId, Guid SMTA1WorkflowhistoryitemId, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
        Task<Either<SMTA1WorkflowHistoryItem, Errors>> Create(SMTA1WorkflowHistoryItem SMTA1Workflowhistoryitem, CancellationToken cancellationToken, IDbContextTransaction transaction = null);
        Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
        Task<IDbContextTransaction> GetTransactionAsync();
        Task<SMTA1WorkflowHistoryItem> ReadForUpdate(Guid id, CancellationToken cancellationToken);
        Task<Errors?> Update(SMTA1WorkflowHistoryItem SMTA1Workflowhistoryitem, CancellationToken cancellationToken);
    }
}