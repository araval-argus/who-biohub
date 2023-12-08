using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;

public interface ISMTA2WorkflowItemReadRepository
{
    Task<IEnumerable<SMTA2WorkflowItem>> List(CancellationToken cancellationToken);
    Task<SMTA2WorkflowItem> Read(Guid id, CancellationToken cancellationToken);
    Task<SMTA2WorkflowItem> ReadByIdAndStatus(Guid id, SMTA2WorkflowStatus status, CancellationToken cancellationToken);
    Task<SMTA2WorkflowItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken);
    Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, SMTA2WorkflowStatus toStatus, SMTA2WorkflowStatus fromStatus, CancellationToken cancellationToken);
    Task<SMTA2WorkflowItem> ReadWithHistory(Guid id, CancellationToken cancellationToken);
}
