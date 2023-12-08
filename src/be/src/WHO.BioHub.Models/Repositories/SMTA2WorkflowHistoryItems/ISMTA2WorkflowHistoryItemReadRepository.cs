using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;

public interface ISMTA2WorkflowHistoryItemReadRepository
{
    Task<IEnumerable<SMTA2WorkflowHistoryItem>> List(CancellationToken cancellationToken);
    Task<IEnumerable<SMTA2WorkflowHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken);
    Task<IEnumerable<SMTA2WorkflowHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken);
    Task<SMTA2WorkflowHistoryItem> Read(Guid id, CancellationToken cancellationToken);
    Task<SMTA2WorkflowHistoryItem> ReadByIdAndStatus(Guid id, SMTA2WorkflowStatus status, CancellationToken cancellationToken);
    Task<SMTA2WorkflowHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken);
}
