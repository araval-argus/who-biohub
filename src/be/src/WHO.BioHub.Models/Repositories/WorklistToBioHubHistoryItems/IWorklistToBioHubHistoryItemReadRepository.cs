using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;

public interface IWorklistToBioHubHistoryItemReadRepository
{
    Task<WorklistToBioHubHistoryItem> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubHistoryItem>> List(CancellationToken cancellationToken);
    Task<WorklistToBioHubHistoryItem> ReadByIdAndStatus(Guid id, WorklistToBioHubStatus status, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken);
    Task<WorklistToBioHubHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken);
}
