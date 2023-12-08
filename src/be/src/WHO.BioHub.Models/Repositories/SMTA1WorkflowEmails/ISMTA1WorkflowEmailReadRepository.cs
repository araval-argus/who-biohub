using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.SMTA1WorkflowEmails
{
    public interface ISMTA1WorkflowEmailReadRepository
    {
        Task<IEnumerable<SMTA1WorkflowEmail>> List(CancellationToken cancellationToken);
        Task<SMTA1WorkflowEmail> Read(Guid id, CancellationToken cancellationToken);
        Task<SMTA1WorkflowEmail> ReadByStatusRoleApproved(SMTA1WorkflowStatus fromStatus, SMTA1WorkflowStatus toStatus, bool approvedSubmission, Guid roleId, CancellationToken cancellationToken, bool isCourier = false);
    }
}