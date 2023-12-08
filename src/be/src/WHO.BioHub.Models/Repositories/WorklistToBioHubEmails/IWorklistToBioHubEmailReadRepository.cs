using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;

public interface IWorklistToBioHubEmailReadRepository
{
    Task<WorklistToBioHubEmail> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubEmail>> List(CancellationToken cancellationToken);
    Task<WorklistToBioHubEmail> ReadByStatusRoleApproved(WorklistToBioHubStatus fromStatus, WorklistToBioHubStatus toStatus, bool approvedSubmission, Guid roleId, CancellationToken cancellationToken, bool isCourier = false);
}
