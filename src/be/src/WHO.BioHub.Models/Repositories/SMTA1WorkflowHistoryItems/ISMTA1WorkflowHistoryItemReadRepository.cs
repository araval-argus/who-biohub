using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems
{
    public interface ISMTA1WorkflowHistoryItemReadRepository
    {
        Task<IEnumerable<SMTA1WorkflowHistoryItem>> List(CancellationToken cancellationToken);
        Task<IEnumerable<SMTA1WorkflowHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken);
        Task<IEnumerable<SMTA1WorkflowHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken);
        Task<SMTA1WorkflowHistoryItem> Read(Guid id, CancellationToken cancellationToken);
        Task<SMTA1WorkflowHistoryItem> ReadByIdAndStatus(Guid id, SMTA1WorkflowStatus status, CancellationToken cancellationToken);
        Task<SMTA1WorkflowHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken);
    }
}