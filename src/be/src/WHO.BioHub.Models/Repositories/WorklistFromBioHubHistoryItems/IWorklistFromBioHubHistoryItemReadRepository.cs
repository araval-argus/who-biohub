using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;

public interface IWorklistFromBioHubHistoryItemReadRepository
{
    Task<WorklistFromBioHubHistoryItem> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubHistoryItem>> List(CancellationToken cancellationToken);
    Task<WorklistFromBioHubHistoryItem> ReadByIdAndStatus(Guid id, WorklistFromBioHubStatus status, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubHistoryItem>> ListByWorklistItemIdWithExtraInfo(Guid worklistItemId, CancellationToken cancellationToken);
    Task<WorklistFromBioHubHistoryItem> ReadByIdWithDocuments(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubHistoryItem>> ListByWorklistItemIdWithDocuments(Guid worklistItemId, CancellationToken cancellationToken);
}
