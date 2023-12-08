using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.SMTA1WorkflowItems
{
    public interface ISMTA1WorkflowItemReadRepository
    {
        Task<IEnumerable<SMTA1WorkflowItem>> List(CancellationToken cancellationToken);
        Task<SMTA1WorkflowItem> Read(Guid id, CancellationToken cancellationToken);
        Task<SMTA1WorkflowItem> ReadByIdAndStatus(Guid id, SMTA1WorkflowStatus status, CancellationToken cancellationToken);
        Task<SMTA1WorkflowItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken);
        Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, SMTA1WorkflowStatus toStatus, SMTA1WorkflowStatus fromStatus, CancellationToken cancellationToken);
        Task<SMTA1WorkflowItem> ReadWithHistory(Guid id, CancellationToken cancellationToken);
    }
}