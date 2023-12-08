using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.SMTA2WorkflowEmails
{
    public interface ISMTA2WorkflowEmailReadRepository
    {
        Task<IEnumerable<SMTA2WorkflowEmail>> List(CancellationToken cancellationToken);
        Task<SMTA2WorkflowEmail> Read(Guid id, CancellationToken cancellationToken);
        Task<SMTA2WorkflowEmail> ReadByStatusRoleApproved(SMTA2WorkflowStatus fromStatus, SMTA2WorkflowStatus toStatus, bool approvedSubmission, Guid roleId, CancellationToken cancellationToken, bool isCourier = false);
    }
}